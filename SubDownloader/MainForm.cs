using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using SubDownloader.Providers;
using System.Net;

namespace SubDownloader
{
    public partial class MainForm : Form
    {

        private string[] SelectedFolders => (from object item in _listBoxWatchedFolders.SelectedItems
                                             select item.ToString()).ToArray();

        public MainForm()
        {
            InitializeComponent();
            Load += Form1_Load;
            _tsBtnRemoveFolder.Enabled = false;
            _activeConnections = 0;
        }

        private void Exit()
        {
            Close();
            Dispose();
        }

        private void Log(string text, Color color = default(Color))
        {
            _rtbLog.Invoke(() =>
            {
                if (Equals(color, default(Color))) color = Color.White;
                if (_rtbLog.Lines.Length > 1000)
                    _rtbLog.Clear();
                _rtbLog.SelectionColor = color;
                _rtbLog.AppendText($"{DateTime.Now}: {text}\n");
                _rtbLog.ScrollToCaret();
            });
        }

        private void StartTimer()
        {
            Log("Starting timer...");
            if (_timer == null)
                _timer = new System.Windows.Forms.Timer();
            _timer.Interval = Data.Instance.UpdateInterval * 60 * 1000;
            _timer.Tick += (EventHandler)((s, e) => SearchSubtitles());
            _timer.Start();
            Log("Timer started.");
        }

        private void StopTimer()
        {
            Log("Stopping timer...");
            _timer?.Stop();
            Log("Timer stopped.");
        }

        private List<SubtitleItem> GetSubtitles(VideoItem videoItem, ISubtitleProvider provider)
        {
            List<SubtitleItem> subtitleItemList = new List<SubtitleItem>();
            foreach (SubtitleItem subtitle in provider.GetSubtitles(videoItem))
            {
                if (subtitle != null)
                {
                    double similarityIndex = Utils.GetSimilarityIndex(subtitle.ExtraInfo, videoItem.ExtraInfo);
                    if (Math.Abs(similarityIndex - 100.0) > -100)
                    {
                        subtitleItemList.Clear();
                        subtitleItemList.Add(subtitle);
                        break;
                    }
                    if (similarityIndex >= 75.0)
                        subtitleItemList.Add(subtitle);
                }
            }
            return subtitleItemList;
        }

        private void InitializeUi()
        {
            InitializeNotify();
            _listBoxWatchedFolders.Items.Clear();
            _listBoxWatchedFolders.Items.AddRange(Data.Instance.WatchedFolders.ToArray());
            //if (_timer.Enabled)
            StopTimer();
            StartTimer();
        }

        private void InitializeNotify()
        {
            _lastWindowState = FormWindowState.Normal;
            Resize += (s, e) =>
            {
                if (WindowState == FormWindowState.Minimized)
                    return;
                _lastWindowState = WindowState;
            };
            //Resize += (s, e) =>
            //{
            //    Hide();
            //};
            _ntfyIcon.MouseDoubleClick += (s, e) =>
            {
                Show();
                WindowState = _lastWindowState;
                _isDoubleClick = true;
            };
            _ntfyIcon.MouseClick += (MouseEventHandler)((s, e) =>
           {
               if (e.Button != MouseButtons.Left)
                   return;
               Thread.Sleep(150);
               Application.DoEvents();
               _isDoubleClick = false;
           });
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add("Exit", (s, e) => Exit());
            _ntfyIcon.ContextMenu = contextMenu;
        }

        private void SearchSubtitles(bool fromTimer = false)
        {
            if (fromTimer)
                StopTimer();
            if (_activeConnections > 0)
                return;
            int[] activeDownloaders = { 0 };
            Action<string> action = SearchSubtitles;
            foreach (string str in _listBoxWatchedFolders.Items)
            {
                ++activeDownloaders[0];

                void Callback(IAsyncResult iar) => --activeDownloaders[0];

                action.BeginInvoke(str, Callback, null);
            }
            ((Action)(() =>
           {
               while (_activeConnections > 0 || activeDownloaders[0] > 0)
                   Thread.Sleep(500);
               StartTimer();
           })).BeginInvoke(null, null);
        }

        private void SearchSubtitles(string folder)
        {
            if (folder == null)
                return;
            Log("Processing folder: " + folder);
            if (!Directory.Exists(folder))
            {
                Log("Folder not exist: " + folder);
            }
            else
            {
                int[] activeConnections = { 0 };
                Log("Loading video items for " + folder);
                List<VideoItem> videoItems = Utils.GetVideoItems(folder);
                Log(videoItems.Count + " video items found for " + folder);
                foreach (VideoItem videoItem1 in videoItems.Where<VideoItem>((Func<VideoItem, bool>)(v => !v.HaveSubtitles)))
                {
                    VideoItem videoItem = videoItem1;
                    //if (!videoItem1.HaveSubtitles)
                    //{
                    while (_activeConnections >= Data.Instance.MaxSimConnections)
                    {
                        Thread.Sleep(100);
                        Application.DoEvents();
                    }
                    ++activeConnections[0];
                    ++_activeConnections;
                    ((Action)(() =>
                   {
                       foreach (ISubtitleProvider subtitlesProvider in Data.Instance.SubtitlesProviders)
                       {
                           Log("Processing " + videoItem.OriginalName + ", provider: " + subtitlesProvider.Name);
                           List<SubtitleItem> subtitles = GetSubtitles(videoItem, subtitlesProvider);
                           if (subtitles.Count > 0)
                           {
                               Log(videoItem.OriginalName + ": Downloading subtitles.");
                               var file = subtitlesProvider.Name.Equals("ScrewZira") ? Utils.DownloadSz(subtitles[0].ID) : Utils.DownloadFile(subtitles[0].Url);
                               if (file != null)
                               {
                                   var str = subtitlesProvider.Name.Equals("ScrewZira") ? file : Utils.Extract(file);
                                   if (str != null)
                                   {
                                       try
                                       {
                                           var ext = Path.GetExtension(str).Equals(".str")
                                               ? ".srt"
                                               : Path.GetExtension(str);
                                           var destFileName = Path.ChangeExtension(videoItem.FileName, ext);
                                           if (destFileName != null) File.Copy(str, destFileName, true);
                                           File.Delete(str);
                                           Log(videoItem.OriginalName + ": Subtitles downloaded.", Color.Green);
                                           break;
                                       }
                                       catch
                                       {
                                           Log(videoItem.OriginalName + ": Error while copying subtitle file.",
                                               Color.Red);
                                       }
                                   }
                                   else
                                       Log(videoItem.OriginalName + ": Extraction error.", Color.Red);
                               }
                               else
                                   Log(videoItem.OriginalName + ": Download error.", Color.Red);
                           }
                           else
                               Log(videoItem.OriginalName + ": No subtitles found.", Color.Red);
                       }
                   })).BeginInvoke(iar =>
                   {
                       --activeConnections[0];
                       --_activeConnections;
                   }, null);
                    //}
                    //else
                    //    Log(videoItem.OriginalName + ": Already Have Subtitle", Color.Cyan);
                }
                while (activeConnections[0] > 0)
                {
                    Thread.Sleep(100);
                }

                Log("Process completed for: " + folder, Color.Gold);
            }
        }

        private bool _initialized;

        private void Form1_Load(object sender, EventArgs e)
        {
            const string url = "https://oribenhur.github.io/Matches";
            var client = new WebClient();
            var values = new string[4];
            var i = 0;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var stream = client.OpenRead(url))
                if (stream != null)
                    using (var reader = new StreamReader(stream))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                            values[i++] = line;
                    }

            Matches.GroupRegex = $@"\b({values[0]})\b";
            Matches.FormatRegex = $@"\b({values[1]})\b";
            Matches.FormatList = values[2];
            Matches.ResolutionList = values[3];
            if (_initialized) return;
            _initialized = true;
            Log("Program loaded, initializing UI...");
            InitializeUi();
            Log("Ready.");
        }

        private void listBoxWatchedFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            _tsBtnRemoveFolder.Enabled = SelectedFolders != null;

        }

        private void _tsBtnAddFolder_Click(object sender, EventArgs e)
        {
            string str = Utils.BrowseForFolder();
            if (str == null || Data.Instance.WatchedFolders.Contains(str))
                return;
            Data.Instance.WatchedFolders.Add(str);
            Data.Save();
            InitializeUi();
        }

        private void _tsBtnRemoveFolder_Click(object sender, EventArgs e)
        {
            if (SelectedFolders == null)
                return;
            foreach (var selectedFolder in SelectedFolders)
            {
                Data.Instance.WatchedFolders.Remove(selectedFolder);
            }
            Data.Save();
            InitializeUi();
        }

        private void _tsBtnSearchSubtitles_Click(object sender, EventArgs e)
        {
            SearchSubtitles();
        }

        private void _tsBtnProgramSettings_Click(object sender, EventArgs e)
        {
            if (new ProgramSettings().ShowDialog() != DialogResult.OK)
                return;
            Log("Saved settings.");
            StopTimer();
            StartTimer();
        }

        private void _listBoxWatchedFolders_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void _listBoxWatchedFolders_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            for (i = 0; i < s.Length; i++)
            {
                _listBoxWatchedFolders.Items.Add(s[i]);
                string str = s[i];
                if (str != null && !Data.Instance.WatchedFolders.Contains(str))
                    Data.Instance.WatchedFolders.Add(str);
            }
            Data.Save();
            InitializeUi();
        }

        private void NewVersion(bool isLoad)
        {
            var downloadUrl = @"";
            XElement change = null;
            Version newVersion = null;
            //object doc2 = null;
            var xmlUrl =
                @"https://onedrive.live.com/download?cid=D9DE3B3ACC374428&resid=D9DE3B3ACC374428%217999&authkey=ADJwQu1VOTfAOVg";
            var appVersion = Assembly.GetExecutingAssembly().GetName().Version;
            var appName = Assembly.GetExecutingAssembly().GetName().Name;
            var doc = new XDocument();
            try
            {
                doc = XDocument.Load(xmlUrl);
                //doc = XDocument.Load(@"D:\OneDrive\update - Copy.xml");

            }
            catch (Exception e)
            {
                using (StreamWriter file =
                    new StreamWriter(@"ErrorLog.txt", true))
                {
                    file.WriteLine(
                        $"---------------------------{DateTime.Now}---------------------------{Environment.NewLine} {e}{Environment.NewLine}");
                }
                Environment.Exit(-1);
            }
            try
            {
                foreach (var dm in doc.Descendants(appName))
                {
                    var versionElement = dm.Element(@"version");
                    if (versionElement == null) continue;
                    var urlelEment = dm.Element(@"url");
                    if (urlelEment == null) continue;
                    newVersion = new Version(versionElement.Value);
                    downloadUrl = urlelEment.Value;
                    change = dm.Element(@"change_log");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + Environment.NewLine + exception.Source);
            }

            if (appVersion.CompareTo(newVersion) < 0)
            {
                //Debug.Assert(change != null, "change != null");
                if (change == null) return;
                change.Value = change.Value;
                var result = MessageBox.Show(
                    $@"{appName.Replace('_', ' ')} v.{newVersion} is out!{Environment.NewLine}{change.Value}",
                    @"New Version is avlibale", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    Process.Start(downloadUrl);
            }
            else
            {
                if (!isLoad)
                    MessageBox.Show(@"You Are Running The Last Version.", @"No New Updates");
            }
        }

        private void cheackForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewVersion(false);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            void Action()
            {
                NewVersion(true);
            }
            var thread = new Thread(Action) { IsBackground = true };
            thread.Start();
        }

        private void _listBoxWatchedFolders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                for (int val = 0; val < _listBoxWatchedFolders.Items.Count; val++)
                {
                    _listBoxWatchedFolders.SetSelected(val, true);
                }
            }
            if (e.KeyCode == Keys.Delete)
            {
                _tsBtnRemoveFolder_Click(sender, e);
            }
        }

        private void _rtbLog_MouseUp(object sender, MouseEventArgs e)
        {

            ContextMenu contextMenu = new ContextMenu();
            MenuItem menuItem = new MenuItem("Clear Log");
            menuItem.Click += Clear;
            contextMenu.MenuItems.Add(menuItem);
            _rtbLog.ContextMenu = contextMenu;

        }

        private void Clear(object sender, EventArgs e)
        {
            _rtbLog.Clear();
            InitializeUi();
        }

        private void Open(object sender, EventArgs e)
        {
            var folder = _listBoxWatchedFolders.SelectedItem;
            Process.Start(folder.ToString());
        }
        private void _listBoxWatchedFolders_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    var rowIndex = GetRowIndex(e.Location);
                    if (rowIndex == -1)
                        break;
                    _listBoxWatchedFolders.ClearSelected();
                    _listBoxWatchedFolders.SelectedIndex = rowIndex;
                    _cmListBox.Show(this, new Point(e.X + 5, e.Y + 35));
                    break;
                case MouseButtons.Left:
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Middle:
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private int GetRowIndex(Point p)
        {
            for (var i = 0; i < _listBoxWatchedFolders.Items.Count; i++)
            {
                var r1 = _listBoxWatchedFolders.GetItemRectangle(i);
                var r = new Rectangle(0, r1.Top, _listBoxWatchedFolders.Width, r1.Height);
                if (!r.Contains(p)) continue;
                _listBoxWatchedFolders.Focus();
                return i;
            }
            return -1;
        }
    }

}

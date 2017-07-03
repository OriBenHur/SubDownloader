namespace SubDownloader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._panelMain = new System.Windows.Forms.Panel();
            this._listBoxWatchedFolders = new System.Windows.Forms.ListBox();
            this._rtbLog = new System.Windows.Forms.RichTextBox();
            this._rtbLogContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._toolStripWatchedFolders = new System.Windows.Forms.ToolStrip();
            this._tsBtnRemoveFolder = new System.Windows.Forms.ToolStripButton();
            this._tsBtnAddFolder = new System.Windows.Forms.ToolStripButton();
            this._tsBtnSearchSubtitles = new System.Windows.Forms.ToolStripButton();
            this._toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._tsBtnProgramSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._toolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._cheackForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ntfyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this._panelMain.SuspendLayout();
            this._toolStripWatchedFolders.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panelMain
            // 
            this._panelMain.Controls.Add(this._listBoxWatchedFolders);
            this._panelMain.Controls.Add(this._rtbLog);
            this._panelMain.Controls.Add(this._toolStripWatchedFolders);
            this._panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelMain.Location = new System.Drawing.Point(0, 0);
            this._panelMain.Name = "_panelMain";
            this._panelMain.Size = new System.Drawing.Size(927, 438);
            this._panelMain.TabIndex = 0;
            // 
            // _listBoxWatchedFolders
            // 
            this._listBoxWatchedFolders.AllowDrop = true;
            this._listBoxWatchedFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listBoxWatchedFolders.FormattingEnabled = true;
            this._listBoxWatchedFolders.Location = new System.Drawing.Point(0, 25);
            this._listBoxWatchedFolders.Name = "_listBoxWatchedFolders";
            this._listBoxWatchedFolders.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this._listBoxWatchedFolders.Size = new System.Drawing.Size(927, 212);
            this._listBoxWatchedFolders.TabIndex = 3;
            this._listBoxWatchedFolders.SelectedIndexChanged += new System.EventHandler(this.listBoxWatchedFolders_SelectedIndexChanged);
            this._listBoxWatchedFolders.DragDrop += new System.Windows.Forms.DragEventHandler(this._listBoxWatchedFolders_DragDrop);
            this._listBoxWatchedFolders.DragEnter += new System.Windows.Forms.DragEventHandler(this._listBoxWatchedFolders_DragEnter);
            this._listBoxWatchedFolders.KeyDown += new System.Windows.Forms.KeyEventHandler(this._listBoxWatchedFolders_KeyDown);
            // 
            // _rtbLog
            // 
            this._rtbLog.BackColor = System.Drawing.Color.Black;
            this._rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._rtbLog.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._rtbLog.DetectUrls = false;
            this._rtbLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._rtbLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._rtbLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._rtbLog.Location = new System.Drawing.Point(0, 237);
            this._rtbLog.Name = "_rtbLog";
            this._rtbLog.ReadOnly = true;
            this._rtbLog.ShortcutsEnabled = false;
            this._rtbLog.ShowSelectionMargin = true;
            this._rtbLog.Size = new System.Drawing.Size(927, 201);
            this._rtbLog.TabIndex = 4;
            this._rtbLog.Text = "";
            this._rtbLog.MouseUp += new System.Windows.Forms.MouseEventHandler(this._rtbLog_MouseUp);
            // 
            // _rtbLogContextMenuStrip
            // 
            this._rtbLogContextMenuStrip.Name = "_rtbLogContextMenuStrip";
            this._rtbLogContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            this._rtbLogContextMenuStrip.Text = "Clear Log";
            //this._rtbLogContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this._rtbLogContextMenuStrip_Opening);
            // 
            // _toolStripWatchedFolders
            // 
            this._toolStripWatchedFolders.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tsBtnRemoveFolder,
            this._tsBtnAddFolder,
            this._tsBtnSearchSubtitles,
            this._toolStripSeparator1,
            this._tsBtnProgramSettings,
            this.toolStripSeparator1,
            this._toolStripDropDownButton});
            this._toolStripWatchedFolders.Location = new System.Drawing.Point(0, 0);
            this._toolStripWatchedFolders.Name = "_toolStripWatchedFolders";
            this._toolStripWatchedFolders.Size = new System.Drawing.Size(927, 25);
            this._toolStripWatchedFolders.TabIndex = 2;
            this._toolStripWatchedFolders.Text = "toolStrip1";
            // 
            // _tsBtnRemoveFolder
            // 
            this._tsBtnRemoveFolder.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._tsBtnRemoveFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._tsBtnRemoveFolder.Image = global::SubDownloader.Properties.Resources.tsBtnRemoveFolder1;
            this._tsBtnRemoveFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsBtnRemoveFolder.Name = "_tsBtnRemoveFolder";
            this._tsBtnRemoveFolder.Size = new System.Drawing.Size(23, 22);
            this._tsBtnRemoveFolder.Text = "Remove";
            this._tsBtnRemoveFolder.ToolTipText = "Remove folder from \"Watched Folder\" list";
            this._tsBtnRemoveFolder.Click += new System.EventHandler(this._tsBtnRemoveFolder_Click);
            // 
            // _tsBtnAddFolder
            // 
            this._tsBtnAddFolder.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._tsBtnAddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._tsBtnAddFolder.Image = global::SubDownloader.Properties.Resources.tsBtnAddFolder_Image;
            this._tsBtnAddFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsBtnAddFolder.Name = "_tsBtnAddFolder";
            this._tsBtnAddFolder.Size = new System.Drawing.Size(23, 22);
            this._tsBtnAddFolder.Text = "Add";
            this._tsBtnAddFolder.ToolTipText = "Add Folder to \"Watched Folder\" list";
            this._tsBtnAddFolder.Click += new System.EventHandler(this._tsBtnAddFolder_Click);
            // 
            // _tsBtnSearchSubtitles
            // 
            this._tsBtnSearchSubtitles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._tsBtnSearchSubtitles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsBtnSearchSubtitles.Name = "_tsBtnSearchSubtitles";
            this._tsBtnSearchSubtitles.Size = new System.Drawing.Size(94, 22);
            this._tsBtnSearchSubtitles.Text = "Search Subtitles";
            this._tsBtnSearchSubtitles.ToolTipText = "Force new subtitles search";
            this._tsBtnSearchSubtitles.Click += new System.EventHandler(this._tsBtnSearchSubtitles_Click);
            // 
            // _toolStripSeparator1
            // 
            this._toolStripSeparator1.Name = "_toolStripSeparator1";
            this._toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _tsBtnProgramSettings
            // 
            this._tsBtnProgramSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._tsBtnProgramSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsBtnProgramSettings.Name = "_tsBtnProgramSettings";
            this._tsBtnProgramSettings.Size = new System.Drawing.Size(101, 22);
            this._tsBtnProgramSettings.Text = "Program settings";
            this._tsBtnProgramSettings.ToolTipText = "Open program settings";
            this._tsBtnProgramSettings.Click += new System.EventHandler(this._tsBtnProgramSettings_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _toolStripDropDownButton
            // 
            this._toolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._toolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cheackForUpdatesToolStripMenuItem});
            this._toolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripDropDownButton.Name = "_toolStripDropDownButton";
            this._toolStripDropDownButton.Size = new System.Drawing.Size(45, 22);
            this._toolStripDropDownButton.Text = "Help";
            // 
            // _cheackForUpdatesToolStripMenuItem
            // 
            this._cheackForUpdatesToolStripMenuItem.Name = "_cheackForUpdatesToolStripMenuItem";
            this._cheackForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this._cheackForUpdatesToolStripMenuItem.Text = "Cheack For Updates";
            this._cheackForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.cheackForUpdatesToolStripMenuItem_Click);
            // 
            // _ntfyIcon
            // 
            this._ntfyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("_ntfyIcon.Icon")));
            this._ntfyIcon.Text = "Subtitle Downloader";
            this._ntfyIcon.Visible = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 438);
            this.Controls.Add(this._panelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Subtitle Downloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this._panelMain.ResumeLayout(false);
            this._panelMain.PerformLayout();
            this._toolStripWatchedFolders.ResumeLayout(false);
            this._toolStripWatchedFolders.PerformLayout();
            this.ResumeLayout(false);

        }
        private bool _isDoubleClick;
        private int _activeConnections;
        private System.Windows.Forms.FormWindowState _lastWindowState;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Panel _panelMain;
        private System.Windows.Forms.ToolStrip _toolStripWatchedFolders;
        private System.Windows.Forms.ToolStripButton _tsBtnRemoveFolder;
        private System.Windows.Forms.ToolStripButton _tsBtnAddFolder;
        private System.Windows.Forms.ListBox _listBoxWatchedFolders;
        private System.Windows.Forms.ToolStripButton _tsBtnSearchSubtitles;
        private System.Windows.Forms.RichTextBox _rtbLog;
        private System.Windows.Forms.ToolStripButton _tsBtnProgramSettings;
        private System.Windows.Forms.ToolStripSeparator _toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton _toolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem _cheackForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.NotifyIcon _ntfyIcon;
        #endregion

        private System.Windows.Forms.ContextMenuStrip _rtbLogContextMenuStrip;
    }
}
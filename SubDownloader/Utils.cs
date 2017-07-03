using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zip;
using Newtonsoft.Json.Linq;
using TMDbLib.Client;
using TVDBSharp;
using System.Configuration;

namespace SubDownloader

{
    public static class Utils
    {
        private static string GetHtml(Uri uri, out Uri responseUri)
        {
            string str = null;
            responseUri = null;
            WebRequest webRequest = WebRequest.Create(uri);
            try
            {
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    responseUri = response.ResponseUri;
                    var stream = response.GetResponseStream();
                    if (stream != null)
                        using (StreamReader streamReader = new StreamReader(stream))
                            str = streamReader.ReadToEnd();
                }
            }
            catch
            {
                // ignored
            }
            return str;
        }

        public static string GetHtml(Uri uri)
        {
            Uri responseUri;
            return GetHtml(uri, out responseUri);
        }

        public static string DownloadFile(Uri uri)
        {
            string str = null;
            try
            {
                string tempFileName = Path.GetTempFileName();
                using (WebClient webClient = new WebClient())
                    webClient.DownloadFile(uri, tempFileName);
                str = tempFileName;
            }
            catch
            {
                // ignored
            }
            return str;
        }

        public static string Extract(string file, bool deleteOriginal = true)
        {
            string str = null;
            if (file != null)
            {
                if (ZipFile.IsZipFile(file, false))
                {
                    try
                    {
                        string tempPath = Path.GetTempPath();
                        ZipFile zipFile = ZipFile.Read(file);
                        if (zipFile.Entries.Count > 0)
                        {
                            ZipEntry zipEntry = zipFile[0];
                            zipEntry.Extract(tempPath, ExtractExistingFileAction.OverwriteSilently);
                            str = Path.Combine(tempPath, zipEntry.FileName);
                        }
                        if (deleteOriginal)
                        {
                            zipFile.Dispose();
                            File.Delete(file);
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            return str;
        }

        public static string BrowseForFolder()
        {
            string str = null;
            var folderBrowserDialog = new FolderSelectDialog();
            var result = folderBrowserDialog.ShowDialog();
            if (result)
                str = folderBrowserDialog.FileName;
            return str;
        }

        public static double GetSimilarityIndex(string str1, string str2)
        {
            var strArray1 = Regex.Split(str1, "[^a-zA-Z0-9]+");
            var strArray2 = Regex.Split(str2, "[^a-zA-Z0-9]+");
            var num1 = Math.Max(strArray1.Length, strArray2.Length);
            var num2 = 0.0;
            foreach (var t in strArray1)
            {
                if (strArray2.Contains(t))
                    ++num2;
            }
            return num2 / num1 * 100.0;
        }

        public static List<VideoItem> GetVideoItems(string folder)
        {
            List<VideoItem> videoItemList = new List<VideoItem>();
            if (Directory.Exists(folder))
            {
                videoItemList.AddRange(
                    from filename in Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories)
                        .Where(s => s.EndsWith(".avi", StringComparison.OrdinalIgnoreCase) ||
                                    (s.EndsWith(".mkv", StringComparison.OrdinalIgnoreCase) ||
                                     s.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase)))
                    let flag =
                    Data.Instance.FileNameFilters.Any(fileNameFilter => filename.ToLower().Contains(fileNameFilter))
                    where !flag
                    select new VideoItem(filename));
            }
            return videoItemList;
        }

        public static bool GetType(string file)
        {
            var sp = new Regex("[sS][0-9]{2}[eE][0-9]{2}");
            var spRegex = new Regex(@"[S][0-9]{2}[E][0-9]{2}");
            var sXregex = new Regex(@"([0-9]{1,2}[xX][0-9]{2})");
            var threeRegex = new Regex(@"(\b\d{3}\b[.])");
            return file != null && (sp.IsMatch(file) || spRegex.IsMatch(file) || sXregex.IsMatch(file) || threeRegex.IsMatch(file));
        }


        internal static string GetId(string jsonUrl, string file, string s, string e)
        {
            var releaseGroup = GetReleaseGroup(file);
            if (GetType(file))
            {

                using (var webClient = new WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    var json = webClient.DownloadString(jsonUrl);
                    var token = JToken.Parse(json);
                    var subsToken = token.SelectToken("subs");
                    var subs = subsToken[s][e];
                    foreach (var sub in subs)
                    {
                        if (sub["release_group"] == null) continue;
                        if (sub["release_group"].ToString().ToLower().Equals(releaseGroup.ToLower()))
                            return sub["id"].ToString();
                    }


                }
            }

            else
            {
                using (var webClient = new WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    var json = webClient.DownloadString(jsonUrl);
                    var token = JToken.Parse(json);
                    var subs = token.SelectToken("subs");
                    foreach (var sub in subs)
                    {
                        if (sub["version"].ToString().ToLower().Equals(file.ToLower()))
                            return sub["id"].ToString();
                    }


                }
            }

            return "";
        }


        private const string CryptTextTvdb = "PImMrxx0MMjVFHdMN1NcdnFS4up3EAvNQVjb3axWKzTMMrLaL22uRw==";
        private const string CryptTextTmdb = "4RqX+Sb+Qcp+tTgHbnYPAlirDXLFV4BPzzGa282upRN2Igmb4frbEnyCNB7Map96m4q/w+l7DalM6VAEJgambQ==";
        internal static string GetImdbId(string file, string name, int year)
        {
            //<Decryption Key> is the part you need for it to work when you compile it yourself 
            //for obvious reasons i didn't include my key 
            var tvdBkey = CryptorEngine.Decrypt(CryptTextTvdb, true, "<Decryption_Key>");
            var tvdBapikey = CryptorEngine.Decrypt(tvdBkey, true, "<Decryption_Key>");
            var tmbDkey = CryptorEngine.Decrypt(CryptTextTmdb, true, "<Decryption_Key>");
            var tmdBapikey = CryptorEngine.Decrypt(tmbDkey, true, "<Decryption_Key>");

            const string imdBid = "";

            if (GetType(file))
            {
                var tvdb = new TVDB(tvdBapikey);
                var searchResults = tvdb.Search(name);

                foreach (var item in searchResults)
                {
                    if (item.Name.ToLower().Equals(name)) return item.ImdbId;
                }
            }

            else
            {
                var tmdb = new TMDbClient(tmdBapikey);
                var movieResult = tmdb.SearchMovieAsync(name, 0, false, year).Result;
                foreach (var movie in movieResult.Results)
                {
                    if (movie.Title.ToLower().Equals(name.ToLower()))
                    {
                        return tmdb.GetMovieAsync(movie.Id).Result.ImdbId;
                    }
                }
            }

            return imdBid;
        }


        private static string GetReleaseGroup(string file)
        {
            var m = Regex.Match(file, Regexs.groupRegex.ToString());
            return m.Value;
        }
        internal static string FixSeriesName(string name)
        {
            if (string.Equals(name, null, StringComparison.Ordinal))
                return null;
            return name.Replace(" ", "-");
        }

        public static int GetYear(string file)
        {
            const string mPattern = "(?:(?:19|20)[0-9]{2})";
            var m = Regex.Match(file, mPattern);
            return Convert.ToInt32(!m.Success ? "0" : m.Value);
        }

    }
}

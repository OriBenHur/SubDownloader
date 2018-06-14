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
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using TMDbLib.Objects.TvShows;
namespace SubDownloader

{
    public static class Utils
    {
        #region Cooment

        //private static string GetHtml(Uri uri, out Uri responseUri)
        //{
        //    string str = null;
        //    responseUri = null;
        //    WebRequest webRequest = WebRequest.Create(uri);
        //    try
        //    {
        //        HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
        //        if (response.StatusCode == HttpStatusCode.OK)
        //        {
        //            responseUri = response.ResponseUri;
        //            var stream = response.GetResponseStream();
        //            if (stream != null)
        //                using (StreamReader streamReader = new StreamReader(stream))
        //                    str = streamReader.ReadToEnd();
        //        }
        //    }
        //    catch
        //    {
        //        // ignored
        //    }
        //    return str;
        //}

        //public static string GetHtml(Uri uri)
        //{
        //    Uri responseUri;
        //    return GetHtml(uri, out responseUri);
        //}

        #endregion


        public static string DownloadSz(string id)
        {
            var json = new JObject();
            var requestJson = new JObject
            {
                {"subtitleID", id}
            };
            json.Add("request", requestJson);
            var client = new RestClient("http://api.screwzira.com/Download");
            var request = new RestRequest { Method = Method.POST };
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept-Encoding", "UTF-8");
            request.Parameters.Clear();
            request.AddParameter("application/json", json.ToString(), ParameterType.RequestBody);
            var tempFileName = Path.GetTempFileName().Replace("tmp", "srt");
            var response = client.Execute(request);
            response.ContentType = "application/force-download";
            if (response.Headers[2].Value.Equals("0")) return null;
            response.RawBytes.SaveAs(tempFileName);
            return tempFileName;
        }

        public static string DownloadFile(Uri uri)
        {

            string str = null;
            try
            {
                var tempFileName = Path.GetTempFileName();
                using (var webClient = new WebClient())
                {
                    webClient.OpenRead(uri);
                    if (Convert.ToInt64(webClient.ResponseHeaders["Content-Length"]) > 0)
                    {
                        webClient.DownloadFile(uri, tempFileName);
                        str = tempFileName;
                    }
                }
                
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
            if (file == null) return null;
            if (!ZipFile.IsZipFile(file, false)) return null;
            try
            {
                var tempPath = Path.GetTempPath();
                var zipFile = ZipFile.Read(file);
                if (zipFile.Entries.Count > 0)
                {
                    var zipEntry = zipFile[0];
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
            return file != null && (sp.IsMatch(file) || spRegex.IsMatch(file) || sXregex.IsMatch(file) ||
                                    threeRegex.IsMatch(file));
        }



        internal static string GetWizdomId(string jsonUrl, string input, string s, string e, VideoItem videoitem)
        {
            #region Cooment

            //Browser browser = new Browser();
            //browser.Goto();
            ////var respons = browser.RequestingSecureUrlRedirectsToLogOn();
            ////var html = respons.GetElementByClassName("choose_subs");
            ////var tmp = html.AsXml();


            ////var doc = new HtmlDocument();
            ////doc.LoadHtml(tmp);
            ////var nodes = doc.DocumentNode.Descendants("choose_subs")
            ////    .Select(y => y.Descendants()
            ////        .Where(x => x.Attributes["class"].Value == "box"))
            ////    .ToList();
            ////var xml = html.AsXml();
            ////xml = xml.Replace("\r\n", String.Empty);
            ////xml = xml.Replace("/* he */", string.Empty);
            ////xml = xml.Replace(" ", String.Empty);
            ////XmlDocument xmlDoc = new XmlDocument();
            ////xmlDoc.LoadXml(xml);
            ////string jsonText = JsonConvert.SerializeXmlNode(xmlDoc);

            #endregion

            var searchTerm = "release_group";
            videoitem.Group = SercheMatch(input, Matches.GroupRegex);
            var searchBy = videoitem.Group;
            //SercheMatch(videoitem.Format, Matches.ResolutionList, true);
            if (videoitem.Group.Equals(""))
            {
                var noGroup = Data.Instance.AutoMode
                    ? DialogResult.OK
                    : MessageBox.Show(
                        $@"Can't Find Release Group in: {Environment.NewLine} {videoitem.FileName} {Environment.NewLine} Would you like me to try and guess?",
                        @"Can't Find Release Group Name", MessageBoxButtons.YesNo);
                if (noGroup == DialogResult.No) return "";
                searchBy = SercheMatch(videoitem.Format.ToLower(), Matches.FormatList.ToLower(), true);
                searchBy = searchBy.Equals("WEB".ToLower()) ? "WEB-DL" : searchBy;
                searchTerm = "format";
            }

            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                var json = webClient.DownloadString(jsonUrl);
                if (json.Equals("")) return "";
                var token = JToken.Parse(json);
                var subsToken = token.SelectToken("subs");
                var subs = videoitem.IsTv ? subsToken[s][e] : token.SelectToken("subs");
                if (subs == null) return "";
                foreach (var sub in subs)
                {
                    if (sub[searchTerm] == null) continue;
                    var subTerm = sub[searchTerm]?.ToString().ToLower();
                    if (sub[searchTerm] == null) continue;
                    if (!subTerm.Equals(searchBy.ToLower())) continue;
                    return sub["id"].ToString();

                }
            }

            return "";
        }

        private static bool HealthCheck(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 5000;
            request.Method = "HEAD";
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }

        internal static string GetImdbId(string name, int year, VideoItem videoItem, ApiKeys apiKeys)
        {

            var tvdBapikey = apiKeys.TvdBapikey;
            var tmdBapikey = apiKeys.TmdBapikey;

            if (videoItem.IsTv)
            {
                if (HealthCheck("http://thetvdb.com"))
                {
                    var tvdb = new TVDB(tvdBapikey);
                    var searchResults = tvdb.Search(name);
                    foreach (var item in searchResults)
                    {
                        if (item.Name.ToLower().Equals(name.ToLower())) return item.ImdbId;
                        if (year <= 0) continue;
                        var tmpName = name.Replace(year.ToString(), string.Empty);
                        tmpName = $@"{tmpName}({year})";
                        if (item.Name.ToLower().Equals(tmpName)) return item.ImdbId;
                    }
                }

                else
                {
                    var tmdb = new TMDbClient(tmdBapikey);
                    var searchResults = tmdb.SearchTvShowAsync(name).Result;
                    foreach (var item in searchResults.Results)
                    {
                        if (item.Name.ToLower().Equals(name.ToLower()))
                        {
                            return tmdb.GetTvShowAsync(item.Id, TvShowMethods.ExternalIds).Result.ExternalIds.ImdbId;
                        }
                    }
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

            return "";
        }

        public static int GetYear(string file)
        {
            const string mPattern = "(?:(?:19|20)[0-9]{2})";
            var m = Regex.Match(file, mPattern);
            return Convert.ToInt32(!m.Success ? "0" : m.Value);
        }

        public static string SercheMatch(string input, string pattern, bool first = false)
        {
            var tmp = @"";
            var index = new List<int>();
            foreach (Match match in Regex.Matches(input.ToLower(), pattern.ToLower(),
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline))
            {
                tmp = match.Value;

                index.Add(input.ToLower().IndexOf(tmp, StringComparison.Ordinal));
                if (first) return tmp;
            }

            if (index.Count == 0) return "";
            var size = index[index.Count - 1] + tmp.Length - index[0];
            if (size - input.Length == 1 || size - input.Length == 0) return input;
            return index.Count > 1 ? input.Substring(index[0], size) : tmp;
        }


        /// <summary>
        /// SzGetTV
        /// </summary>
        /// <param name="searchPhrase">	ImdbID / Film name / Exact film release</param>
        /// <param name="episode">Episode number</param>
        /// <param name="season">Season number Key</param>
        /// <param name="searchType">FilmName, Subtitle, ImdbID</param>
        /// <param name="version">1.0</param>
        /// <param name="year">	Year of release (yyy)</param>
        /// <returns></returns>
        public static string SzGetTv(string searchPhrase, int episode, int season, int year = 0, string searchType = "ImdbID", string version = "1.0")
        {
            var json = new JObject();
            var requestJson = new JObject
            {
                {"SearchPhrase", searchPhrase},
                {"SearchType", searchType},
                {"Episode", episode},
                {"Season", season},
                {"Year", year},
                {"Version", version}
            };
            json.Add("request", requestJson);
            var client = new RestClient("http://api.screwzira.com/FindSeries");
            //var client = new RestClient("http://api.screwzira.com/FindFilm");
            var request = new RestRequest { Method = Method.POST };
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", json.ToString(), ParameterType.RequestBody);
            var response = client.Execute(request);
            var deserializeObjectJson = (string)JsonConvert.DeserializeObject(response.Content);
            var token = JObject.Parse(deserializeObjectJson);
            var results = token.SelectToken("Results");
            return results.HasValues ? deserializeObjectJson : null;
        }


        /// <summary>
        /// SzGetMovie
        /// </summary>
        /// <param name="searchPhrase">	ImdbID / Film name / Exact film release</param>
        /// <param name="searchType">FilmName, Subtitle, ImdbID</param>
        /// <param name="version">1.0</param>
        /// <param name="year">	Year of release (yyyy)</param>
        /// <returns></returns>
        public static string SzGetMovie(string searchPhrase, int year = 0, string searchType = "ImdbID", string version = "1.0")
        {
            var json = new JObject();
            var requestJson = new JObject
            {
                {"SearchPhrase", searchPhrase},
                {"SearchType", searchType},
                {"Year", year},
                {"Version", version}
            };
            json.Add("request", requestJson);
            var client = new RestClient("http://api.screwzira.com/FindFilm");
            var request = new RestRequest { Method = Method.POST };
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", json.ToString(), ParameterType.RequestBody);
            var response = client.Execute(request);
            var deserializeObjectJson = (string)JsonConvert.DeserializeObject(response.Content);
            var token = JObject.Parse(deserializeObjectJson);
            var results = token.SelectToken("Results");
            return results.HasValues ? deserializeObjectJson : null;

        }


        public static string[] GetSZid(VideoItem videoitem, string json)
        {
            try
            {
                var emptyGroup = false;
                var searchBy = videoitem.Group;
                var token = JObject.Parse(json);
                var subs = token.SelectToken("Results");
                if (videoitem.Group.Equals(""))
                {
                    var noGroup = Data.Instance.AutoMode
                        ? DialogResult.OK
                        : MessageBox.Show(
                            $@"Can't Find Release Group in: {Environment.NewLine} {videoitem.FileName} {Environment.NewLine} Would you like me to try and guess?",
                            @"Can't Find Release Group Name", MessageBoxButtons.YesNo);
                    if (noGroup == DialogResult.No) return null;
                    searchBy = SercheMatch(videoitem.Format.ToLower(), Matches.FormatList.ToLower(), true);
                    searchBy = searchBy.Equals("WEB".ToLower()) ? "WEB-DL" : searchBy;
                    emptyGroup = true;
                }

                return (from sub in subs
                    let subSearchBy =
                        (emptyGroup
                            ? SercheMatch(sub["SubtitleName"].ToString(), Matches.FormatList.ToLower(), true)
                            : SercheMatch(sub["SubtitleName"].ToString(), Matches.GroupRegex)).ToLower()
                    where subSearchBy.Equals(searchBy)
                    select new[] {sub["Identifier"].ToString(), sub["SubtitleName"].ToString()}).FirstOrDefault();
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}
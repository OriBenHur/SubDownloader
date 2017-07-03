using System.IO;
using System.Text.RegularExpressions;

namespace SubDownloader
{
    public class VideoItem
    {
        public string FileName { get; private set; }

        public string OriginalName { get; private set; }

        public bool HaveSubtitles
        {
            get
            {
                if (!File.Exists(Path.ChangeExtension(FileName, ".srt")))
                    return File.Exists(Path.ChangeExtension(FileName, ".sub"));
                return true;
            }
        }

        public string Name { get; private set; }

        public int Season { get; private set; }

        public int Episode { get; private set; }

        private int Episode2 { get; set; }

        public string ExtraInfo { get; private set; }

        public bool IsTV { get; set; }

        public VideoItem(string filename, string name = null)
        {
            if (filename == null)
                return;
            FileName = filename;
            OriginalName = name ?? Path.GetFileName(filename);
            ParseName();
        }

        private void ParseName()
        {
            IsTV = Utils.GetType(OriginalName);
            if (IsTV)
            {
                var match = Regex.Match(OriginalName, "^(?:.*\\\\)?(?<series>[^\\\\]+?)[ _.\\-\\[]+(?:[s]?(?<season>\\d+)[ _.\\-\\[\\]]*[ex](?<episode>\\d+)|(?:\\#|\\-\\s)(?<season>(?!(?:\\d{4}.\\d{2}.\\d{2}|\\d{2}.\\d{2}.\\d{4}))\\d+)\\.(?<episode>\\d+))(?:[ _.+-]+(?:[s]?\\k<season>[ _.\\-\\[\\]]*[ex](?<episode2>\\d+)|(?:\\#|\\-\\s)\\k<season>\\.(?<episode2>\\d+))|(?:[ _.+-]*[ex+-]+(?<episode2>\\d+)))*[ _.\\-\\[\\]]*(?<title>(?![^\\\\].*?(?<!the)[ .(-]sample[ .)-]).*?)\\.(?<ext>[^.]*)$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);
                int result1;
                int.TryParse(match.Groups["season"].Value, out result1);
                int result2;
                int.TryParse(match.Groups["episode"].Value, out result2);
                int result3;
                int.TryParse(match.Groups["episode2"].Value, out result3);
                Name = ApplyTranslators(match.Groups["series"].Value.CleanString().ToLower());
                ExtraInfo = match.Groups["title"].Value.CleanString().ToLower();
                Season = result1;
                Episode = result2;
                Episode2 = result3;
            }
            else
            {
                //var tmp = OriginalName.Split('.');
                var tmpRegex = new Regex("(19|20)[0-9][0-9].*");
                var filename = tmpRegex.Replace(OriginalName, string.Empty);
                filename = filename.EndsWith(".") ? filename.Substring(0, filename.Length - 1) : filename;
                ExtraInfo = OriginalName.Replace(filename+".", string.Empty);

                Name = ApplyTranslators(filename.CleanString().ToLower()); 
            }

        }

        private string ApplyTranslators(string str)
        {
            foreach (string key in Data.Instance.CustomNameTranslator.Keys)
                str = str.Replace(key, Data.Instance.CustomNameTranslator[key]);
            return str;
        }
    }
}

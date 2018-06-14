using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SubDownloader
{
    public class VideoItem
    {
        public string FileName { get; }

        public string OriginalName { get; }

        public bool HaveSubtitles => File.Exists(Path.ChangeExtension(FileName, ".srt")) || File.Exists(Path.ChangeExtension(FileName, ".sub"));

        public string Name { get; private set; }

        public int Season { get; private set; }

        public int Episode { get; private set; }

        public string ExtraInfo { get; private set; }

        public bool IsTv { get; private set; }

        public string Format { get; set; }

        public string Group { get; set; }

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
            IsTv = Utils.GetType(OriginalName);
            if (IsTv)
            {
                var match = Regex.Match(OriginalName, "^(?:.*\\\\)?(?<series>[^\\\\]+?)[ _.\\-\\[]+(?:[s]?(?<season>\\d+)[ _.\\-\\[\\]]*[ex](?<episode>\\d+)|(?:\\#|\\-\\s)(?<season>(?!(?:\\d{4}.\\d{2}.\\d{2}|\\d{2}.\\d{2}.\\d{4}))\\d+)\\.(?<episode>\\d+))(?:[ _.+-]+(?:[s]?\\k<season>[ _.\\-\\[\\]]*[ex](?<episode2>\\d+)|(?:\\#|\\-\\s)\\k<season>\\.(?<episode2>\\d+))|(?:[ _.+-]*[ex+-]+(?<episode2>\\d+)))*[ _.\\-\\[\\]]*(?<title>(?![^\\\\].*?(?<!the)[ .(-]sample[ .)-]).*?)\\.(?<ext>[^.]*)$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);
                int.TryParse(match.Groups["season"].Value, out int result1);
                int.TryParse(match.Groups["episode"].Value, out int result2);
                //int.TryParse(match.Groups["episode2"].Value, out int result3);
                Name = ApplyTranslators(match.Groups["series"].Value.CleanString().ToLower());
                ExtraInfo = match.Groups["title"].Value.CleanString().ToLower();
                Season = result1;
                Episode = result2;
            }
            else
            {
                //var tmp = OriginalName.Split('.');
                var tmpRegex = new Regex("(19|20)[0-9][0-9].*");
                var filename = tmpRegex.Replace(OriginalName, string.Empty);
                filename = filename.EndsWith(".") ? filename.Substring(0, filename.Length - 1) : filename;
                ExtraInfo = OriginalName.Replace(filename + ".", string.Empty);
                Name = ApplyTranslators(filename.CleanString().ToLower());
            }

        }

        public static string ApplyTranslators(string str)
        {
            return Data.Instance.CustomNameTranslator.Keys.Aggregate(str, (current, key) => Regex.Replace(current, key, Data.Instance.CustomNameTranslator[key], RegexOptions.IgnoreCase));
        }
    }
}

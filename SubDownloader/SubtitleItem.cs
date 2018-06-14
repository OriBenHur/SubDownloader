using System;

namespace SubDownloader
{
    public class SubtitleItem : VideoItem
    {
        public Uri Url { get; }
        public string ID { get; }

        public SubtitleItem(string name, Uri url, string extra = "")
          : base(AddExtension(name))
        {
            Url = url;
            ID = extra;
        }

        private static string AddExtension(string name)
        {
            if (name != null && (!name.EndsWith(".srt") || !name.EndsWith(".idx") || !name.EndsWith(".sub")))
                return name + ".srt";
            return name;
        }
    }
}

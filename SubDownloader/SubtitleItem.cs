using System;

namespace SubDownloader
{
  public class SubtitleItem : VideoItem
  {
    public Uri Url { get; private set; }

    public SubtitleItem(string name, Uri url)
      : base(AddExtension(name), null)
    {
      Url = url;
    }

    private static string AddExtension(string name)
    {
      if (name != null && (!name.EndsWith(".srt") || !name.EndsWith(".idx") || !name.EndsWith(".sub")))
        return name + ".srt";
      return name;
    }
  }
}

using System.Collections.Generic;

namespace SubDownloader.Providers
{
  public interface ISubtitleProvider
  {
    string Name { get; }

    IEnumerable<SubtitleItem> GetSubtitles(VideoItem videoItem);

  }
}

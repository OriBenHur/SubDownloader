// Decompiled with JetBrains decompiler
// Type: SubtitleDownloader.Providers.ISubtitleProvider
// Assembly: SubtitleDownloader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 303B1D91-376D-46F0-BE96-AABE09279493
// Assembly location: C:\Users\Ori\Downloads\SubtitleDownloader\SubtitleDownloader.exe

using System.Collections.Generic;

namespace SubDownloader.Providers
{
  public interface ISubtitleProvider
  {
    string Name { get; }

    IEnumerable<SubtitleItem> GetSubtitles(VideoItem videoItem);
  }
}

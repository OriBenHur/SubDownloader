// Decompiled with JetBrains decompiler
// Type: SubtitleDownloader.SubtitleItem
// Assembly: SubtitleDownloader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 303B1D91-376D-46F0-BE96-AABE09279493
// Assembly location: C:\Users\Ori\Downloads\SubtitleDownloader\SubtitleDownloader.exe

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

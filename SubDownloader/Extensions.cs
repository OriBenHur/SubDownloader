// Decompiled with JetBrains decompiler
// Type: SubtitleDownloader.Extensions
// Assembly: SubtitleDownloader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 303B1D91-376D-46F0-BE96-AABE09279493
// Assembly location: C:\Users\Ori\Downloads\SubtitleDownloader\SubtitleDownloader.exe

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SubDownloader
{
  public static class Extensions
  {
    public static void Invoke(this Control ctrl, Action action)
    {
      if (ctrl.InvokeRequired)
        ctrl.BeginInvoke(action);
      else
        action();
    }

    public static KeyValuePair<string, string> PairWith(this string str, string value)
    {
      return new KeyValuePair<string, string>(str, value);
    }

    public static void AsErrorMessage(this string msg, string caption = "Error")
    {
      int num = (int) MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }

    public static void AsInfoMessage(this string msg, string caption = "Info")
    {
      int num = (int) MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }

    public static string CleanString(this string str)
    {
      return str.Replace(".", " ").Replace("-", " ").Replace(":", "");
    }

    public static IComparer<T> Reverse<T>(this IComparer<T> comparer)
    {
      return new ReverseComparer<T>(comparer);
    }

    public class ReverseComparer<T> : IComparer<T>
    {
      private readonly IComparer<T> _original;

      public ReverseComparer(IComparer<T> original)
      {
        _original = original;
      }

      public int Compare(T item1, T item2)
      {
        return _original.Compare(item2, item1);
      }
    }
  }
}

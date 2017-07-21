using System;
using System.Collections.Generic;
using System.IO;

namespace SubDownloader.Providers
{
    public class Wizdom : ISubtitleProvider
    {
        public string Name => "Wizdom";

        public IEnumerable<SubtitleItem> GetSubtitles(VideoItem videoItem)
        {
            var api = new ApiKeys();
            var se = "";
            var ep = "";
            var yearSize = 0;
            var name = videoItem.Name;
            var season = videoItem.Season;
            if (season > 0)
                se = season < 10 ? "s0" + season : "s" + season;

            var episode = videoItem.Episode;
            if (episode > 0)
                ep = episode < 10 ? "e0" + episode : "e" + episode;

            var file = Path.GetFileNameWithoutExtension(videoItem.FileName);
            if (file == null) yield break;
            var year = Utils.GetYear(file);

            if (year > 0)
                yearSize = season > 0 && episode > 0 ? year.ToString().Length - Name.Length + 2 : year.ToString().Length;

            var size = name.Length + yearSize + se.Length + ep.Length + 1;
            var tmpfile = file.Substring(size);
            tmpfile = tmpfile.StartsWith(".") ? tmpfile.Substring(1) : tmpfile;
            videoItem.Format = Utils.SercheMatch(tmpfile, Matches.FormatRegex);

            var imdBid = Utils.GetImdbId(name, year, videoItem, api);
            if (imdBid.Equals("")) yield break;
            Uri.TryCreate(new Uri("http://json.wizdom.xyz/"), "[].json".Replace("[]", imdBid), out Uri uri);

            var index = file.LastIndexOf(videoItem.Format, StringComparison.Ordinal);
            var tFile = file.Substring(index);
            tFile = tFile.StartsWith("-") ? tFile.Substring(1) : tFile;
            tFile = tFile.Replace(videoItem.Format, string.Empty);
            var id = Utils.GetId(uri.ToString(), tFile, season.ToString(), episode.ToString(), videoItem);
            if (id == "") yield break;
            if (Uri.TryCreate(new Uri("http://zip.wizdom.xyz/"), "[].zip".Replace("[]", id), out Uri url))
                yield return new SubtitleItem(file, url);
        }
    }
}

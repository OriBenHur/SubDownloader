using System;
using System.Collections.Generic;
using System.IO;


namespace SubDownloader.Providers
{
    public class ScrewZira : ISubtitleProvider
    {
        public string Name => "ScrewZira";

        public IEnumerable<SubtitleItem> GetSubtitles(VideoItem videoItem)
        {
            var api = new ApiKeys();
            var se = "";
            var ep = "";
            var yearSize = 0;
            var name = videoItem.Name;
            var season = videoItem.Season;

            var isTv = videoItem.IsTv;
            if (season > 0)
                se = season < 10 ? "s0" + season : "s" + season;

            var episode = videoItem.Episode;
            if (episode > 0)
                ep = episode < 10 ? "e0" + episode : "e" + episode;

            var file = VideoItem.ApplyTranslators(Path.GetFileNameWithoutExtension(videoItem.FileName));
            if (file == null) yield break;
            var year = Utils.GetYear(file);

            if (year > 0)
                yearSize = season > 0 && episode > 0 ? year.ToString().Length - Name.Length + 2 : year.ToString().Length;

            var size = name.Length + yearSize + se.Length + ep.Length + 1;
            var tmpfile = file.Substring(size);
            tmpfile = tmpfile.StartsWith(".") ? tmpfile.Substring(1) : tmpfile;
            videoItem.Format = Utils.SercheMatch(tmpfile, Matches.FormatRegex);
            videoItem.Group = Utils.SercheMatch(tmpfile, Matches.GroupRegex);
            var imdBid = Utils.GetImdbId(name, year, videoItem, api);
            if (imdBid.Equals("")) yield break;
            //var json = isTv ? Utils.SzGetTv(imdBid, episode, season, year) : Utils.SzGetMovie(imdBid, year);
            //if (json == null) yield break;

            var id = Utils.GetSZid(videoItem, isTv ? Utils.SzGetTv(imdBid, episode, season, year) : Utils.SzGetMovie(imdBid, year));
            if (id == null) yield break;

            //yield return id;
            var url = new Uri("http://dummy");
            yield return new SubtitleItem(id[1], url, id[0]);
        }
    }
}
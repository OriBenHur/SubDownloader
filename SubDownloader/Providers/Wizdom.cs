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
            var name = Utils.FixSeriesName(videoItem.Name);
            var season = videoItem.Season;
            var episode = videoItem.Episode;
            var file = Path.GetFileNameWithoutExtension(videoItem.FileName);
            var year = Utils.GetYear(file);
            var imdBid = Utils.GetImdbId(file, name.Replace("-", " "), year);
            if (imdBid.Equals("")) yield break;
            var uri = new Uri("http://json.wizdom.xyz/[].json".Replace("[]", imdBid));
            var id = Utils.GetId(uri.ToString(), file, season.ToString(), episode.ToString());
            if (id == "") yield break;
            Uri url;
            if (Uri.TryCreate(new Uri("http://zip.wizdom.xyz/"), "[].zip".Replace("[]", id), out url))
                yield return new SubtitleItem(file, url);
        }
    }
}

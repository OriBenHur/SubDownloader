using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SubDownloader.Providers;

namespace SubDownloader
{
    [Serializable]
    public class Data
    {
        private static readonly string SavePath = Directory.GetCurrentDirectory() + "\\Data.sd";
        private static Data _instance;

        public int MaxSimConnections { get; set; }

        public int UpdateInterval { get; set; }

        public Dictionary<string, string> CustomNameTranslator { get; set; }

        public List<string> FileNameFilters { get; private set; }

        public List<string> WatchedFolders { get; private set; }

        public bool AutoMode { get; set; } 

        public List<ISubtitleProvider> SubtitlesProviders => new List<ISubtitleProvider>
        {
            //new Wizdom(),
            new ScrewZira(),
            new Wizdom()
            //new ScrewZira()

        };

        public static Data Instance => _instance ?? (_instance = Load());

        private Data()
        {
            CheckData();
        }

        private void CheckData()
        {
            if (WatchedFolders == null)
                WatchedFolders = new List<string>();
            if (FileNameFilters == null)
                FileNameFilters = new List<string>();
            if (MaxSimConnections == 0)
                MaxSimConnections = 1;
            if (UpdateInterval == 0)
                UpdateInterval = 15;
            if (CustomNameTranslator != null)
                return;
            CustomNameTranslator = new Dictionary<string, string>();
        }

        public static void Save()
        {
            try
            {
                if (File.Exists(SavePath))
                    File.Delete(SavePath);
            }
            catch (Exception ex)
            {
                $"Cannot delete {SavePath}, {ex.Message}".AsErrorMessage("Serialization error");
                return;
            }
            try
            {
                using (FileStream fileStream = File.OpenWrite(SavePath))
                    new BinaryFormatter().Serialize(fileStream, _instance);
            }
            catch (Exception ex)
            {
                $"Cannot write to {SavePath}, {ex.Message}".AsErrorMessage("Serialization error");
            }
        }

        private static Data Load()
        {
            if (!File.Exists(SavePath))
                return new Data();
            object obj = null;
            try
            {
                using (FileStream fileStream = File.OpenRead(SavePath))
                    obj = new BinaryFormatter().Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                $"Cannot read {SavePath}, {ex.Message}".AsErrorMessage("Deserialization error");
            }
            if ((obj == null ? 1 : (!(obj is Data) ? 1 : 0)) != 0)
            {
                try
                {
                    File.Delete(SavePath);
                }
                catch (Exception)
                {
                    //ignored
                }
                return new Data();
            }
            Data data = obj as Data;
            data?.CheckData();
            return data;
        }
    }
}

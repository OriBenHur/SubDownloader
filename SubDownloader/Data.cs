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
        private static string _savePath = Directory.GetCurrentDirectory() + "\\Data.sd";
        private static Data _instance;

        public int MaxSimConnections { get; set; }

        public int UpdateInterval { get; set; }

        public Dictionary<string, string> CustomNameTranslator { get; set; }

        public List<string> FileNameFilters { get; set; }

        public List<string> WatchedFolders { get; set; }

        public List<ISubtitleProvider> SubtitlesProviders => new List<ISubtitleProvider>
        {
            new Wizdom()

        };

        public static Data Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Load();
                return _instance;
            }
        }

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
                if (File.Exists(_savePath))
                    File.Delete(_savePath);
            }
            catch (Exception ex)
            {
                string.Format("Cannot delete {0}, {1}", _savePath, ex.Message).AsErrorMessage("Serialization error");
                return;
            }
            try
            {
                using (FileStream fileStream = File.OpenWrite(_savePath))
                    new BinaryFormatter().Serialize(fileStream, _instance);
            }
            catch (Exception ex)
            {
                string.Format("Cannot write to {0}, {1}", _savePath, ex.Message).AsErrorMessage("Serialization error");
            }
        }

        private static Data Load()
        {
            if (!File.Exists(_savePath))
                return new Data();
            object obj = null;
            try
            {
                using (FileStream fileStream = File.OpenRead(_savePath))
                    obj = new BinaryFormatter().Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                string.Format("Cannot read {0}, {1}", _savePath, ex.Message).AsErrorMessage("Deserialization error");
            }
            if ((obj == null ? 1 : (!(obj is Data) ? 1 : 0)) != 0)
            {
                try
                {
                    File.Delete(_savePath);
                }
                catch (Exception)
                {
                }
                return new Data();
            }
            Data data = obj as Data;
            if (data != null)
                data.CheckData();
            return data;
        }
    }
}

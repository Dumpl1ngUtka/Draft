using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Services.SaveLoadSystem
{
    public class SaveLoadService : MonoBehaviour
    {
        private static SaveLoadService _instance;
        
        private string _dungeonSaveDirectory;
        private ISaveLoadRepository<LocationData> _dungeonRepository;
        public static SaveLoadService Instance => _instance == null ? 
            _instance = FindAnyObjectByType<SaveLoadService>() : _instance;

        public void Init(ISaveLoadRepository<LocationData> dungeonRepository)
        {
            _dungeonRepository = dungeonRepository;
            _dungeonSaveDirectory = Path.Combine(Application.persistentDataPath, "Saves/Dungeon/savefile.json");
            
            if (!Directory.Exists(_dungeonSaveDirectory))
                Directory.CreateDirectory(_dungeonSaveDirectory);
            

            GetAdviceByKey("");
            GetErrorByKey("");
        }

        public void SaveDungeonData(LocationData data) => 
            _dungeonRepository.SaveDataTo(data, _dungeonSaveDirectory);
        
        public LocationData LoadDungeonData() =>
            _dungeonRepository.LoadDataFrom(_dungeonSaveDirectory);
        
        
        #region CSV

        private Dictionary<string, PanelData> _adviceCache;
        private Dictionary<string, PanelData> _errorCache;
        private const string _advicePath = "CSV/advices_en"; 
        private const string _errorPath = "CSV/errors_en"; 
        
        public PanelData GetAdviceByKey(string key)
        {
            return FindDataInCache(key, _adviceCache, _advicePath);
        }
        
        public PanelData GetErrorByKey(string key)
        {
            return FindDataInCache(key, _errorCache, _errorPath);
        }

        private PanelData FindDataInCache(string key, Dictionary<string, PanelData> cache, string path)
        {
            if (cache == null)
            {
                cache = new Dictionary<string, PanelData>();
                var dataset = Resources.Load<TextAsset>(path);
                var dataLines = dataset.text.Split('\n');
                foreach (var line in dataLines)
                {
                    var data = line.Split(';');
                    if (data.Length == 0)
                        continue;

                    var panelName = "";
                    try { panelName = data[1];}
                    catch {;}
                    
                    var description = "";
                    try { description = data[2];}
                    catch {;}
                    
                    var color = "#3D372B";
                    try
                    {
                        color = data[3].Remove(color.Length - 1, 1);
                    }
                    catch {;}
                    
                    var panelData = new PanelData(panelName, description, color);
                    cache.Add(data[0], panelData);
                }
            }

            if (!cache.TryGetValue(key, out var result))
                result = new PanelData("empty_" + key, "", "");

            return result;
        }

        #endregion
    }
}
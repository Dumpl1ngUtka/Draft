using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Services.SaveLoadSystem
{
    public class SaveLoadService : MonoBehaviour
    {
        private const string _saveDirectory = "Saves/";
        private const string _dungeonSaveFileName = "dungeon.json";
        private const string _belongingSaveFileName = "belonging.json";
        private const string _runDataFileName = "run.json";
        
        private static SaveLoadService _instance;

        private string _savePath;
        private ISaveLoadRepository<LocationData> _dungeonRepository;
        private ISaveLoadRepository<BelongingData> _belongingRepository;
        private ISaveLoadRepository<RunData> _runRepository;
        public static SaveLoadService Instance => _instance == null ? 
            _instance = FindAnyObjectByType<SaveLoadService>() : _instance;

        public void Init(
            ISaveLoadRepository<LocationData> dungeonRepository,
            ISaveLoadRepository<BelongingData> belongingRepository,
            ISaveLoadRepository<RunData> runRepository)
        {
            _dungeonRepository = dungeonRepository;
            _belongingRepository = belongingRepository;
            _runRepository = runRepository;
            
            _savePath = Path.Combine(Application.persistentDataPath, _saveDirectory);
            if (!Directory.Exists(_savePath))
                Directory.CreateDirectory(_savePath);

            GetAdviceByKey("");
            GetErrorByKey("");
        }

        public void SaveDungeonData(LocationData data) => 
            _dungeonRepository.SaveDataTo(data, _savePath + _dungeonSaveFileName);
        
        public LocationData LoadDungeonData() =>
            _dungeonRepository.LoadDataFrom(_savePath + _dungeonSaveFileName);
        
        public void SaveUnitsBelongingData(BelongingData data) => 
            _belongingRepository.SaveDataTo(data, _savePath + _belongingSaveFileName);
        
        public BelongingData LoadUnitsBelongingData() =>
            _belongingRepository.LoadDataFrom(_savePath + _belongingSaveFileName);
        
        public void SaveRunData(RunData data) => 
            _runRepository.SaveDataTo(data, _savePath + _runDataFileName);
        
        public RunData LoadRunData() =>
            _runRepository.LoadDataFrom(_savePath + _runDataFileName);
        
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
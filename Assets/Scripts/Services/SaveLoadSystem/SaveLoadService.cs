using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services.SaveLoadSystem
{
    public class SaveLoadService : MonoBehaviour
    {
        private readonly string _saveFilePrefix = "savefile";
        private readonly string _saveFileExtension = ".json";
        private string _saveDirectory;
        private ISaveLoadRepository _repository ;
        public static SaveLoadService Instance { get; private set; }

        public void Init(ISaveLoadRepository repository)
        {
            Instance = FindAnyObjectByType<SaveLoadService>();
            _repository = repository;
            
            _saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
            
            if (!Directory.Exists(_saveDirectory))
                Directory.CreateDirectory(_saveDirectory);

            GetAdviceByKey("");
        }

        public void SavePlayerData()
        {
            /*var player = FindAnyObjectByType<Player>();
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            GetNewSavePath(out var savePath, out var fileName);
            
            var position = player.transform.position;
            var health = player.Health.CurrentHealth;
            var data = new SaveData(fileName, health, currentSceneIndex, position);
            
            _repository.SaveDataTo(data, savePath);*/
        }

        public SaveData GetPlayerData(string saveFileName)
        {
            var savePath = Path.Combine(_saveDirectory, saveFileName + _saveFileExtension);
            return _repository.LoadDataFrom(savePath);
        }
        
        public List<SaveData> GetSaveFilesList()
        {
            var directoryInfo = new DirectoryInfo(_saveDirectory);
            var saveFiles = directoryInfo.GetFiles("*" + _saveFileExtension);

            var gameDataList = new List<SaveData>();
            foreach (var file in saveFiles)
            {
                var savePath = Path.Combine(_saveDirectory, file.Name);
                var data = _repository.LoadDataFrom(savePath);
                gameDataList.Add(data);
            }

            return gameDataList;
        }

        public SaveData GetLatestSaveData()
        {
            var saves = GetSaveFilesList();
            var max = saves.Max(entry => entry.SaveDate);
            var latestSave = saves.Where(entry => entry.SaveDate == max).ToArray()[0];
            return latestSave;
        }

        public SaveData GetNewSaveData()
        {
            GetNewSavePath(out var savePath, out var fileName);

            var position = new Vector3(-138f, 2f, -145f);
            var data = new SaveData(fileName, 100, 0, position);
            
            return data;
        }

        private void GetNewSavePath(out string savePath, out string fileName)
        {
            var saveNumber = 0;
            do
            {
                saveNumber++;
                fileName = _saveFilePrefix + saveNumber;
                savePath = Path.Combine(_saveDirectory,fileName + _saveFileExtension);
            } while (File.Exists(savePath));
        }

        #region CSV

        private Dictionary<string, PanelData> _adviceCache;
        private Dictionary<string, PanelData> _errorCache;
        private const string _advicePath = "CSV/Advice"; 
        private const string _errorPath = "CSV/Error"; 
        
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
                    
                    var panelData = new PanelData(panelName, description);
                    cache.Add(data[0], panelData);
                }
            }

            if (!cache.TryGetValue(key, out var result))
                result = new PanelData("empty_" + key, "");

            return result;
        }

        #endregion
    }
}
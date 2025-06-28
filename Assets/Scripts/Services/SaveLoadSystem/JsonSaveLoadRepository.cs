using System;
using System.IO;
using UnityEngine;

namespace Services.SaveLoadSystem
{
    public class JsonSaveLoadRepository<T> : ISaveLoadRepository<T> where T : new()
    {
        public T LoadDataFrom(string path)
        {
            if (!File.Exists(path))
                return new T();
            
            var json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
        
        public void SaveDataTo(T data, string path)
        {
            var json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);        
        }
    }
    
    [Serializable]
    public class SerializationWrapper<T>
    {
        public T[] array;
    
        public SerializationWrapper(T[] array)
        {
            this.array = array;
        }
    }
}
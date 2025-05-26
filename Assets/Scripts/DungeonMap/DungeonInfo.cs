using System;
using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonMap
{
    [Serializable]
    public struct DungeonInfo
    {
        [Header("Dungeon Base Data")] 
        public int ID;
        public string Name;
        public string Description;
        public bool IsCompleted;
        [Header("Path Generate Data")]
        public int LineCount;
        public int ColumnCount;
        public int PathCount;
        [SerializeField] private PathCellChances[] PathCellChances;
        [SerializeField] PathCellRule[] PathCellRules;
        [Header("Other Data")]
        public List<Class> Classes;
        public List<Race> Races;

        public List<EnemyPositionPreset> GetEnemyPositionPresets()
        {
            var path = "DungeonPresets/" + ID;
            return Resources.LoadAll<EnemyPositionPreset>(path).ToList();
        }

        public EnemyPositionPreset GetEnemyPositionPreset()
        {
            var presets = GetEnemyPositionPresets();
            return presets[Random.Range(0, presets.Count)];
        }

        public PathCellType GetRandomPathCellType(int lineIndex)
        {
            foreach (var rule in PathCellRules)
            {
                if (rule.LineIndex == lineIndex)
                    return rule.Type;
            }
            
            var sumFrequency = PathCellChances.Sum(chance => chance.Frequency);
            var randomNum = Random.Range(0, sumFrequency) + 1;
            for (int i = 0; i < PathCellChances.Length; i++)
            {
                randomNum -= PathCellChances[i].Frequency;
                if (randomNum <= 0)
                    return PathCellChances[i].Type;
            }
            return PathCellChances[^1].Type;
        }
    }
}
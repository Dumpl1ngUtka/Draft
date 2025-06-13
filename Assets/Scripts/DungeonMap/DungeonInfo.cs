using System;
using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace DungeonMap
{
    [Serializable]
    public struct DungeonInfo
    {
        [Header("Dungeon Base Data")] 
        public int ID;
        public Sprite Image;
        public string Name;
        public string Description;
        public bool IsCompleted;
        [Header("Path Generate Data")]
        public int LineCount;
        public int ColumnCount;
        public int StartPointCount;
        [Range(0f,1f)] public float BranchingChance;
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
            return presets[new Random().Next(presets.Count)];
        }

        public PathCellType GetRandomPathCellType(int lineIndex, Random random)
        {
            foreach (var rule in PathCellRules)
            {
                if (rule.LineIndex == lineIndex)
                    return rule.Type;
            }
            
            var sumFrequency = PathCellChances.Sum(chance => chance.Frequency);
            var randomNum = random.Next(0, sumFrequency) + 1;
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
using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using UnityEngine;

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
        [Header("Other Data")]
        public List<Class> Classes;
        public List<Race> Races;

        public List<EnemyPositionPreset> GetEnemyPositionPresets()
        {
            var path = "DungeonPresets/" + ID;
            return Resources.LoadAll<EnemyPositionPreset>(path).ToList();
        }
    }
}
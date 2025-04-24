using System;
using System.Collections.Generic;
using Battle.Units;

namespace DungeonMap
{
    [Serializable]
    public struct DungeonInfo
    {
        public string Name;
        public string Description;
        public bool IsCompleted;
        
        public List<Class> Classes;
        public List<Race> Races;
    }
}
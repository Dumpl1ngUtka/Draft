using System;
using UnityEngine.Serialization;

namespace DungeonMap
{
    [Serializable]
    public struct PathCellChances
    {
        public PathCellType Type;
        public int Frequency;
    }
}
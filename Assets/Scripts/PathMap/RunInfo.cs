using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;

namespace PathMap
{
    public class RunInfo
    {
        public int[] Path { get; private set; }
        public int[] SelectedPath { get; private set; }
        public Unit[] PlayerUnits { get; private set; }

        public void UpdatePath(int newIndex)
        {
            var newPath = new int[Path.Length + 1];
            for (var i = 0; i < Path.Length; i++)
            {
                newPath[i] = Path[i];
            }
            newPath[^1] = newIndex;
            Path = newPath;
        }

        public void SavePlayerUnits(IEnumerable<Unit> playerUnits)
        {
            PlayerUnits = playerUnits.ToArray();
        }
    }
}
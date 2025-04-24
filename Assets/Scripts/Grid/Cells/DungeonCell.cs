using DungeonMap;
using UnityEngine;

namespace Grid.Cells
{
    public class DungeonCell : GridCell
    {
        [SerializeField] private DungeonInfo _dungeonInfo;
        public DungeonInfo DungeonInfo => _dungeonInfo;
    }
}
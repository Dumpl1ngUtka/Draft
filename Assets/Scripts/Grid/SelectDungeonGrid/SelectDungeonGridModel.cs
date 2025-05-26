using DungeonMap;
using PathMap;

namespace Grid.SelectDungeonGrid
{
    public class SelectDungeonGridModel : GridModel
    {
        public void DungeonSelected(DungeonInfo info)
        {
            GameControlService.CurrentDungeonInfo = info;
            GameControlService.CurrentRunInfo = new RunInfo();
            GameControlService.ChangeGrid(GameControlService.DraftGridPrefab);
        }
    }
}
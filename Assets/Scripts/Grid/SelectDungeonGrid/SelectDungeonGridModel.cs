using DungeonMap;
using PathMap;
using Services.SaveLoadSystem;

namespace Grid.SelectDungeonGrid
{
    public class SelectDungeonGridModel : GridModel
    {
        public LocationData LocationData { get; private set; } 
            = SaveLoadService.Instance.LoadDungeonData();

        public void DungeonSelected(DungeonInfo info)
        {
            GameControlService.CurrentDungeonInfo = info;
            GameControlService.CurrentRunInfo = new RunInfo();
            GameControlService.ChangeGrid(GameControlService.DraftGridPrefab);
        }
    }
}
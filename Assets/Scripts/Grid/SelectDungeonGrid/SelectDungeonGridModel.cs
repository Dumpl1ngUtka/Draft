using DungeonMap;
using Services.SaveLoadSystem;

namespace Grid.SelectDungeonGrid
{
    public class SelectDungeonGridModel : GridModel
    {
        public LocationData LocationData { get; private set; } 
            = SaveLoadService.Instance.LoadDungeonData();

        public void DungeonSelected(DungeonInfo info)
        {
            SaveLoadService.Instance.SaveRunData(new RunData(info.ID));
            GameControlService.ChangeGrid(GameControlService.DraftGridPrefab);
        }
    }
}
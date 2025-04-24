using DungeonMap;
using Services.GameControlService;

namespace Grid.SelectDungeonGrid
{
    public class SelectDungeonGridModel : GridModel
    {
        public void DungeonSelected(DungeonInfo info)
        {
            GameControlService.Instance.FinishDungeonSelectLevel(info);
        }
    }
}
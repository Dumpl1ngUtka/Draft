using DungeonMap;
using Services.GameControlService;
using Services.GameControlService.GridStateMachine;

namespace Grid.SelectDungeonGrid
{
    public class SelectDungeonGridModel : GridModel
    {
        public SelectDungeonGridModel(GridStateMachine stateMachine) : base(stateMachine)
        {
        }

        public void DungeonSelected(DungeonInfo info)
        {
            GameControlService.Instance.CurrentDungeonInfo = info;
            StateMachine.ChangeGrid(StateMachine.DraftGrid);
        }
    }
}
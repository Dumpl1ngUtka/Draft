using System.Linq;
using Battle.Grid;
using DungeonMap;
using Grid.Cells;
using Services.GameControlService.GridStateMachine;
using Services.PanelService;

namespace Grid.SelectDungeonGrid
{
    public class SelectDungeonGridController : GridController
    {
        private SelectDungeonGridModel _model;
        private SelectDungeonGridView _view;

        public override void Init(GridStateMachine gridStateMachine)
        {
            base.Init(gridStateMachine);
            _model = new SelectDungeonGridModel(gridStateMachine);
            _view = GetComponent<SelectDungeonGridView>();
            SubscribeToCells(_view.Cells.Select(x => x as GridCell).ToList());
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        protected override void DraggedFromCell(GridCell startDraggingCell, GridCell overCell)
        {
        }

        protected override void DraggedToCell(GridCell startDraggingCell, GridCell overCell)
        {
        }

        protected override void DoubleClicked(GridCell cell)
        {
        }

        protected override void HoldFinished(GridCell cell)
        {
        }

        protected override void HoldBegin(GridCell from)
        {
        }

        protected override void Clicked(GridCell cell)
        {
            var info = ((DungeonCell)cell).DungeonInfo;
            PanelService.Instance.InstantiateDungeonInfoPanel(info, OnApplyDungeonButtonPressed);
        }

        private void OnApplyDungeonButtonPressed(DungeonInfo info)
        {
            _model.DungeonSelected(info);
        }

        protected override void DragFinished(GridCell from, GridCell to)
        {
        }
    }
}
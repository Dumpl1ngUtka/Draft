using System.Linq;
using DungeonMap;
using Grid.Cells;
using Services.PanelService;

namespace Grid.SelectDungeonGrid
{
    public class SelectDungeonGridController : GridController
    {
        private SelectDungeonGridModel _model;
        private SelectDungeonGridView _view;

        public override void Enter()
        {
            base.Enter();
            _model = new SelectDungeonGridModel();
            _view = GetComponent<SelectDungeonGridView>();
            foreach (var cell in _view.Cells)
                cell.SetAlphaHitTestMinimumThreshold(0.5f);
            SubscribeToCells(_view.Cells.Select(x => x as GridCell).ToList()); 
            //_view.ControlCellSize();
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
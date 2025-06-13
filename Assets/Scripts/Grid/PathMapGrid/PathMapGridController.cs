using System.Linq;
using Grid.Cells;
using UnityEngine;

namespace Grid.PathMapGrid
{
    public class PathMapGridController : GridController
    {
        private PathMapGridModel _model;
        private PathMapGridView _view;

        public override void Enter()
        {
            base.Enter();
            _model = new PathMapGridModel();
            _view = GetComponent<PathMapGridView>();
            _view.GenerateMap(_model.Map, _model.Path);
            var gridCells = _view.Cells.SelectMany(pathMapCells => pathMapCells).Select(cell => cell as GridCell).ToList();
            SubscribeToCells(gridCells);
        }

        public override void Exit()
        {
            base.Exit();
            var gridCells = _view.Cells.SelectMany(pathMapCells => pathMapCells).Select(cell => cell as GridCell).ToList();
            UnsubscribeToCells(gridCells);
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
            var pathCell = cell as PathMapCell;
            if (pathCell != null && pathCell.IsEnabled)
                _model.SelectPathCell(pathCell);
        }

        protected override void DragFinished(GridCell from, GridCell to)
        {
        }
    }
}
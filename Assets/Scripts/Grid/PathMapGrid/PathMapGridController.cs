using Grid.Cells;
using Services.GameControlService.GridStateMachine;
using UnityEngine;

namespace Grid.PathMapGrid
{
    public class PathMapGridController : GridController
    {
        private PathMapGridModel _model;
        private PathMapGridView _view;

        public override void Init(GridStateMachine gridStateMachine)
        {
            base.Init(gridStateMachine);
            _model = new PathMapGridModel(gridStateMachine);
            _view = GetComponent<PathMapGridView>();
        }

        public override void OnEnter()
        {
            _view.GenerateEmptyMap(_model.Paths);
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
            
        }

        protected override void DragFinished(GridCell from, GridCell to)
        {
        }
    }
}
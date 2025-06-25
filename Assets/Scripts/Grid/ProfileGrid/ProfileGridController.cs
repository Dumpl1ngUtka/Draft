using Grid.Cells;
using UnityEngine;

namespace Grid.ProfileGrid
{
    [RequireComponent(typeof(ProfileGridView))]
    public class ProfileGridController : GridController
    {
        private ProfileGridModel _model;
        private ProfileGridView _view;
        
        public override void Enter()
        {
            base.Enter();
            _model = new ProfileGridModel();
            _view = GetComponent<ProfileGridView>();
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
using Grid.Cells;

namespace Grid.PathMapGrid
{
    public class PathMapGridController : GridController
    {
        private PathMapGridModel _model;
        private PathMapGridView _view;

        protected override void OnActivate()
        {
        }

        public override void Init()
        {
            base.Init();
            _model = new PathMapGridModel();
            _view = GetComponent<PathMapGridView>();
            var cells = _view.GenerateMap();
            _model.GeneratePathsFor(cells);
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
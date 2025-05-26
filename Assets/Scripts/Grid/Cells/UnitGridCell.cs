using Battle.Grid.Visualization;
using Units;

namespace Grid.Cells
{
    public class UnitGridCell : GridCell
    {
        public Unit Unit { get; private set; }
        public UnitGridCellRenderer Renderer { get; private set; }
        public GridPosition Position { get; private set; }
        public bool IsActive { get; private set; }

        public void Init(int lineIndex, int columnIndex, TeamType teamType)
        {
            Renderer = GetComponent<UnitGridCellRenderer>();
            Renderer.Init();
            IsActive = true;
            Position = new GridPosition(lineIndex, columnIndex, teamType);
        }

        public void Deactivate()
        {
            IsActive = false;
            Destroy(Renderer);
        }
        
        public void AddUnit(Unit unit)
        {
            Unit = unit;
            Unit.Position = Position;
            Renderer.SubscribeToUnit(unit);
            Renderer.Render(unit);
        }

        public void RemoveUnit()
        {
            if (Unit == null)
                return;
            
            Renderer.UnsubscribeFromUnit();
            Renderer.Render(null);
            Unit = null;
        }
    }
}
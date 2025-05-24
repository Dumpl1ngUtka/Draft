using Battle.Grid.Visualization;
using Battle.Units;
using Units;

namespace Grid.Cells
{
    public class UnitGridCell : GridCell
    {
        public TeamType TeamType { get; private set; }
        public int LineIndex { get; private set; }
        public int ColumnIndex { get; private set; }
        public Unit Unit { get; private set; }
        public UnitGridCellRenderer Renderer { get; private set; }
        
        public void Init(int lineIndex, int columnIndex, TeamType teamType)
        {
            Renderer = GetComponent<UnitGridCellRenderer>();
            Renderer.Init();
            
            LineIndex = lineIndex;
            ColumnIndex = columnIndex;
            TeamType = teamType;
        }
        
        public void AddUnit(Unit unit)
        {
            Unit = unit;
            Unit.SetTeam(TeamType);
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
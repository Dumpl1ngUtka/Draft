using Battle.Grid.Visualization;
using Battle.Units;

namespace Grid.Cells
{
    public class UnitGridCell : GridCell
    {
        public TeamType TeamType { get; private set; }
        public int LineIndex { get; private set; }
        public int ColumnIndex { get; private set; }
        public Unit Unit { get; private set; }
        public UnitGridCellRenderer Renderer { get; private set; }
        
        private GridType _gridType;
        
        public void Init(int lineIndex, int columnIndex, TeamType teamType, GridType preset)
        {
            Renderer = GetComponent<UnitGridCellRenderer>();
            Renderer.Init();
            _gridType = preset;
            
            LineIndex = lineIndex;
            ColumnIndex = columnIndex;
            TeamType = teamType;

            Renderer.SetActive(GridType.None);
        }
        
        public void AddUnit(Unit unit)
        {
            Unit = unit;
            Unit.SetTeam(TeamType);
            Renderer.SetActive(_gridType);
            Renderer.SubscribeToUnit(unit);
            Renderer.Render(unit);
        }

        public void RemoveUnit()
        {
            if (Unit == null)
                return;
            
            Renderer.UnsubscribeFromUnit();
            Renderer.SetActive(GridType.None);
            Renderer.Render(null);
            Unit = null;
        }
    }
}
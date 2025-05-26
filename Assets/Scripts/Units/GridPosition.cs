using Grid;

namespace Units
{
    public struct GridPosition
    {
        public readonly int LineIndex;
        public readonly int ColumnIndex;
        public readonly TeamType TeamType;

        public GridPosition(int lineIndex, int columnIndex, TeamType teamType)
        {
            LineIndex = lineIndex;
            ColumnIndex = columnIndex;
            TeamType = teamType;
        }

        public bool OwnEquals(GridPosition other)
        {
            return LineIndex == other.LineIndex 
                   && ColumnIndex == other.ColumnIndex
                   && TeamType == other.TeamType;
        }
    }
}
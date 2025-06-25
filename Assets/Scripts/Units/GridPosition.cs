using System;
using Grid;

namespace Units
{
    [Serializable]
    public struct GridPosition
    {
        public int _teamType;
        public int LineIndex;
        public int ColumnIndex;
        public TeamType TeamType => (TeamType)_teamType;

        public GridPosition(int lineIndex, int columnIndex, TeamType teamType)
        {
            LineIndex = lineIndex;
            ColumnIndex = columnIndex;
            _teamType = (int)teamType;
        }

        public bool OwnEquals(GridPosition other)
        {
            return LineIndex == other.LineIndex
                   && ColumnIndex == other.ColumnIndex
                   && TeamType == other.TeamType;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Battle.InfoPanel;
using Battle.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Grid
{
    public abstract class Grid : MonoBehaviour
    {
        protected const int LineCount = 3;
        protected const int ColumnCount = 3;
        protected const int PlayerTeamID = 0;
        protected const int EnemyTeamID = 1;
        
        [SerializeField] private GridCell _cellPrefab;
        [SerializeField] private ErrorPanel _errorPanelPrefab;

        public abstract void Init();
        
        protected List<GridCell> InitiateCells(int lineCount, int colunmCount, int teamIndex, Transform container)
        {
            var cells = new List<GridCell>();

            for (int line = 0; line < lineCount; line++)
            {
                for (int column = 0; column < colunmCount; column++)
                {
                    var cell = Instantiate(_cellPrefab, container);
                    cell.Init( line, column ,teamIndex);
                    cell.DragFinished += DragFinished;
                    cell.DragOverCell += DragOverCell;
                    cell.DragBegin += DragBegin;
                    cell.Clicked += Clicked;
                    cells.Add(cell);
                }
            }
            
            return cells;
        }

        protected abstract void DragOverCell(GridCell from, GridCell over);

        protected abstract void DragBegin(GridCell from);

        protected abstract void Clicked(GridCell cell);

        protected abstract void DragFinished(GridCell from, GridCell to);

        protected void InstantiateErrorPanel(string errorID)
        {
            _errorPanelPrefab.Instantiate(errorID);
        }
    }
}

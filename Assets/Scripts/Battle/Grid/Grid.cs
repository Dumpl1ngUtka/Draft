using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Grid.Visualization;
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
        protected GridVisualizer GridVisualizer;
        
        [SerializeField] private CellPresetType _cellPreset;
        [SerializeField] private GridCell _cellPrefab;
        [SerializeField] private ErrorPanel _errorPanelPrefab;

        public virtual void Init()
        {
            GridVisualizer = new GridVisualizer();
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
            if (active)
                GridVisualizer.PlayEffect(this);
        }
        
        protected List<GridCell> InitiateCells(int lineCount, int colunmCount, int teamIndex, Transform container)
        {
            ClearConteiner(container);
            
            var cells = new List<GridCell>();

            for (int line = 0; line < lineCount; line++)
            {
                for (int column = 0; column < colunmCount; column++)
                {
                    var cell = Instantiate(_cellPrefab, container);
                    cell.Init( line, column ,teamIndex, _cellPreset);
                    cell.DragFinished += DragFinished;
                    cell.DragOverCell += DragOverCell;
                    cell.HoldFinished += HoldFinished;
                    cell.HoldBegin += HoldBegin;
                    cell.Clicked += Clicked;
                    cell.DoubleClicked += DoubleClicked;
                    cells.Add(cell);
                }
            }
            GridVisualizer.AddCells(cells);
            return cells;
        }

        private void ClearConteiner(Transform container)
        {
            foreach(Transform child in container.transform)
            {
                Destroy(child.gameObject);
            }
        }

        protected abstract void DoubleClicked(GridCell cell);

        protected abstract void HoldFinished(GridCell cell);

        protected abstract void DragOverCell(GridCell from, GridCell over);

        protected abstract void HoldBegin(GridCell from);

        protected abstract void Clicked(GridCell cell);

        protected abstract void DragFinished(GridCell from, GridCell to);

        protected void InstantiateErrorPanel(string errorID)
        {
            _errorPanelPrefab.Instantiate(errorID);
        }
    }
}

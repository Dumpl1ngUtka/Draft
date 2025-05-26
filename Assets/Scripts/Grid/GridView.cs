using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.Grid.Visualization;
using Grid.Cells;
using Services.GlobalAnimation;
using UnityEngine;
using UnityEngine.UI;

namespace Grid
{
    public class GridView : MonoBehaviour
    {
        private const int LineCount = 3;
        private const int ColumnCount = 3;
        
        [SerializeField] private UnitGridCell _cellPrefab;
        [SerializeField] protected UnitGridCellContainer[] Containers;
        private GridVisualizer _gridVisualizer;
        
        public GridVisualizer Visualizer
        {
            get
            {
                if (_gridVisualizer == null)
                {
                    _gridVisualizer = new GridVisualizer();
                    var cells = new List<UnitGridCell>();
                    foreach (var container in Containers)
                        cells.AddRange(container.GetCells());
                    _gridVisualizer.AddCells(cells);
                }
                return _gridVisualizer;
            }
        }

        public List<UnitGridCell> GetUnitsCellsByTeam(TeamType type)
        {
            var unitGridCellContainer = Containers.First(container => container.TeamType == type);
            return unitGridCellContainer.GetCells();
        }
        
        public List<UnitGridCell> InitiateUnitCells()
        {
            var cells = new List<UnitGridCell>();
            
            foreach (var container in Containers)
            {
                ClearContainer(container.Container);

                for (int line = 0; line < LineCount; line++)
                {
                    for (int column = 0; column < ColumnCount; column++)
                    {
                        var cell = Instantiate(_cellPrefab, container.Container);
                        cell.Init(line, column, container.TeamType);
                        cells.Add(cell);
                    }
                }
            }

            SetContaindersSize();

            return cells;
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void SetContaindersSize()
        {
            foreach (var container in Containers)
            {
                var layoutGroup = container.Container.GetComponent<GridLayoutGroup>();
                var rectTransform = container.Container.transform as RectTransform;
                if (layoutGroup == null)
                    continue;
                
                var containerWight = rectTransform.rect.width;
                var containerHeight = rectTransform.rect.height;
                layoutGroup.cellSize = Vector2.one * 
                                       Mathf.Min(containerHeight / layoutGroup.constraintCount, 
                                           (containerWight * 0.9f) / layoutGroup.constraintCount);
            }
        }

        protected void ClearContainer(Transform container)
        {
            foreach(Transform child in container.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
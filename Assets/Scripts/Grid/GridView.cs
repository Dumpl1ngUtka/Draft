using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.Grid.Visualization;
using Grid.Cells;
using UnityEngine;

namespace Grid
{
    public class GridView : MonoBehaviour
    {
        private const int LineCount = 3;
        private const int ColumnCount = 3;
        
        [SerializeField] private GridType _type;
        [SerializeField] private UnitGridCell _cellPrefab;
        [SerializeField] protected UnitGridCellContainer[] Containers;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
            //if (isActive)
                //GridVisualizer.PlayEffect(this);
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
                        cell.Init(line, column, container.TeamType, _type);
                        cells.Add(cell);
                    }
                }
            }

            return cells;
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
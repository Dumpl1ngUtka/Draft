using System.Collections.Generic;
using System.Linq;
using Battle.Grid.Visualization;
using Grid.Cells;
using UnityEngine;
using UnityEngine.Serialization;

namespace Grid
{
    public abstract class GridController : MonoBehaviour
    {
        private GridView _baseView;

        public void SetActive(bool isActive)
        {
            _baseView.SetActive(isActive);
        }
        
        public virtual void Init()
        {
            _baseView = gameObject.GetComponent<GridView>();
            var cells = _baseView.InitiateUnitCells();
            SubscribeToCells(cells.Select(x => x as GridCell).ToList());
        }

        protected void SubscribeToCells(List<GridCell> cells)
        {
            foreach (var cell in cells)
            {
                cell.DragFinished += DragFinished;
                cell.DraggedToCell += DraggedToCell;
                cell.DraggedFromCell += DraggedFromCell;
                cell.HoldFinished += HoldFinished;
                cell.HoldBegin += HoldBegin;
                cell.Clicked += Clicked;
                cell.DoubleClicked += DoubleClicked;
            }
        }
        
        protected abstract void DraggedFromCell(GridCell startDraggingCell, GridCell overCell);

        protected abstract void DraggedToCell(GridCell startDraggingCell, GridCell overCell);
        
        protected abstract void DoubleClicked(GridCell cell);

        protected abstract void HoldFinished(GridCell cell);
        
        protected abstract void HoldBegin(GridCell from);

        protected abstract void Clicked(GridCell cell);

        protected abstract void DragFinished(GridCell from, GridCell to);
    }
}
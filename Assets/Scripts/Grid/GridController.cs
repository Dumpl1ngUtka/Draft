using System.Collections.Generic;
using System.Linq;
using Grid.Cells;
using Services.GameControlService.GridStateMachine;
using UnityEngine;

namespace Grid
{
    public abstract class GridController : MonoBehaviour
    {
        private GridView _baseView;
        
        protected GridStateMachine GridStateMachine;

        // ReSharper disable Unity.PerformanceAnalysis
        public void SetActive(bool isActive)
        {
            _baseView.SetActive(isActive);
        }
        
        public virtual void Init(GridStateMachine gridStateMachine)
        {
            GridStateMachine = gridStateMachine;
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
        
        public abstract void OnEnter();
        public abstract void OnExit();
        
        protected abstract void DraggedFromCell(GridCell startDraggingCell, GridCell overCell);

        protected abstract void DraggedToCell(GridCell startDraggingCell, GridCell overCell);
        
        protected abstract void DoubleClicked(GridCell cell);

        protected abstract void HoldFinished(GridCell cell);
        
        protected abstract void HoldBegin(GridCell from);

        protected abstract void Clicked(GridCell cell);

        protected abstract void DragFinished(GridCell from, GridCell to);
    }
}
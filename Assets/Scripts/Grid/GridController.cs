using System.Collections.Generic;
using Grid.Cells;
using Services.GlobalAnimation;
using UnityEngine;

namespace Grid
{
    public abstract class GridController : MonoBehaviour
    {
        protected void SubscribeToCells(List<GridCell> cells)
        {
            foreach (var cell in cells)
            {
                cell.DragFinished += (gridCell, cell1) =>  Debug.Log("DragFinished");
                cell.DraggedToCell += (gridCell, cell1) =>  Debug.Log("DraggedToCell");
                cell.DraggedFromCell += (gridCell, cell1) =>  Debug.Log("DraggedFromCell");
                cell.HoldFinished += (gridCell) =>  Debug.Log("HoldFinished");
                cell.HoldBegin+= (gridCell) =>  Debug.Log("HoldBegin");
                cell.Clicked+= (gridCell ) =>  Debug.Log("Clicked");
                cell.DoubleClicked += (gridCell ) =>  Debug.Log("DoubleClicked");
                
                cell.DragFinished += DragFinished;
                cell.DraggedToCell += DraggedToCell;
                cell.DraggedFromCell += DraggedFromCell;
                cell.HoldFinished += HoldFinished;
                cell.HoldBegin += HoldBegin;
                cell.Clicked += Clicked;
                cell.DoubleClicked += DoubleClicked;
            }
        }
        
        protected void UnsubscribeToCells(List<GridCell> cells)
        {
            foreach (var cell in cells)
            {
                cell.DragFinished -= DragFinished;
                cell.DraggedToCell -= DraggedToCell;
                cell.DraggedFromCell -= DraggedFromCell;
                cell.HoldFinished -= HoldFinished;
                cell.HoldBegin -= HoldBegin;
                cell.Clicked -= Clicked;
                cell.DoubleClicked -= DoubleClicked;
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public virtual void Enter()
        {
            GlobalAnimationSevice.Instance.PlayRandomTransitionAnimaton(false);
        }
        
        public virtual void Exit()
        {
            GlobalAnimationSevice.Instance.PlayRandomTransitionAnimaton(true);
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
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Grid.Cells
{
    public class GridCell : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private const float _doubleClickTime = 0.1f;
        private const float _holdTime = 0.2f;

        private int _clickCount;
        private GridCell _dragOverCell;
        
        public Action<GridCell, GridCell> DraggedToCell;
        public Action<GridCell, GridCell> DraggedFromCell;
        public Action<GridCell, GridCell> DragFinished;
        public Action<GridCell> Clicked;
        public Action<GridCell> DoubleClicked;
        public Action<GridCell> HoldBegin;
        public Action<GridCell> HoldFinished;

        private Coroutine _doubleClickRoutine;
        private Coroutine _holdRoutine;
        private bool _isInteracting;
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected IEnumerator TimerRoutine(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback.Invoke();
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            StopCoroutine(_holdRoutine);

            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out UnitGridCell cell) && cell != this)
            {
                DragFinished?.Invoke(this, cell);
                return;
            }

            if (_isInteracting)
            {
                HoldFinished?.Invoke(this);
                _isInteracting = false;
                return;
            }

            if (_clickCount++ == 0)
            {
                _doubleClickRoutine = StartCoroutine(TimerRoutine(_doubleClickTime * 2, OneTouch));
            }
            else
            {
                StopCoroutine(_doubleClickRoutine);
                DoubleTouch();
            }
            
        }
        
        private void OneTouch()
        {
            _clickCount = 0;
            Clicked?.Invoke(this);
        }

        private void DoubleTouch()
        {
            _clickCount = 0;
            DoubleClicked?.Invoke(this);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            var wasOverCell = _dragOverCell != null;
            var isOverCell = eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out UnitGridCell cell);
            var isOurCell = cell == this;
            
            if (isOverCell && !wasOverCell)
            {
                _dragOverCell = cell;
                DraggedToCell?.Invoke(this, cell);
            }
            else if (!isOurCell)
            {
                DraggedFromCell?.Invoke(this, _dragOverCell);
                _dragOverCell = null;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _holdRoutine = StartCoroutine(TimerRoutine(_holdTime, Hold));
        }

        private void Hold()
        {
            _isInteracting = true;
            HoldBegin?.Invoke(this);
        }
    }
}
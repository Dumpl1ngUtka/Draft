using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Grid.Cells
{
    public class GridCell : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private const float _doubleClickTime = 0.2f;
        private const float _holdTime = 0.3f;

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
        private bool _isHolding;
        private bool _isDoubleClicking = false;
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected IEnumerator TimerRoutine(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback.Invoke();
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            StopCoroutine(_holdRoutine);

            var gameObj = eventData.pointerCurrentRaycast.gameObject;
            if (gameObj != null && gameObj.TryGetComponent(out UnitGridCell cell) && cell != this)
            {
                DragFinished?.Invoke(this, cell);
                return;
            }

            if (_isHolding)
            {
                HoldFinished?.Invoke(this);
                _isHolding = false;
                return;
            }

            if (_isDoubleClicking)
            {
                _isDoubleClicking = false;
                return;
            }

            if (_clickCount++ == 0) 
                _doubleClickRoutine = StartCoroutine(TimerRoutine(_doubleClickTime, OneTouch));
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
            
            var gameObj = eventData.pointerCurrentRaycast.gameObject;
            if (gameObj == null)
            {
                if (_dragOverCell != null)
                {
                    DraggedFromCell?.Invoke(this, _dragOverCell);
                    _dragOverCell = null;
                }
                return;
            }

            var nowOnCell = gameObj.TryGetComponent(out UnitGridCell cell);
            
            if (nowOnCell && !wasOverCell)
            {
                _dragOverCell = cell;
                DraggedToCell?.Invoke(this, cell);
            }
            else if ((!nowOnCell ) && wasOverCell)
            {
                DraggedFromCell?.Invoke(this, _dragOverCell);
                _dragOverCell = null;
            }
            else if (cell != null && cell != _dragOverCell)
            {
                DraggedFromCell?.Invoke(this, _dragOverCell);
                _dragOverCell = null;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_clickCount > 0)
            {
                _isDoubleClicking = true;
                StopCoroutine(_doubleClickRoutine);
                DoubleTouch();
                return;
            }
            
            _holdRoutine = StartCoroutine(TimerRoutine(_holdTime, Hold));
        }

        private void Hold()
        {
            _isHolding = true;
            HoldBegin?.Invoke(this);
        }
    }
}
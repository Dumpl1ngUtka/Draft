using System;
using System.Collections;
using Battle.DamageSystem;
using Battle.Grid.Visualization;
using Battle.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Grid
{
    public class GridCell : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private int _clickCount;
        private float _doubleClickTime = 0.1f;
        private float _holdTime = 0.2f;
        private GridCell _dragOverCell;
        
        public int TeamIndex { get; private set; }
        public int LineIndex { get; private set; }
        public int ColumnIndex { get; private set; }
        public Unit Unit { get; private set; }
        public GridCellRenderer Renderer { get; private set; }
        
        public Action<GridCell, GridCell> DragOverCell;
        public Action<GridCell, GridCell> DragFinished;
        public Action<GridCell> Clicked;
        public Action<GridCell> DoubleClicked;
        public Action<GridCell> HoldBegin;
        public Action<GridCell> HoldFinished;

        private Coroutine _doubleClickRoutine;
        private Coroutine _holdRoutine;
        private bool _isInteracting;
        private CellPresetType _cellPresetType;
        
        private IEnumerator TimerRoutine(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback.Invoke();
        }
        
        public void Init(int lineIndex, int columnIndex,int teamIndex, CellPresetType preset)
        {
            Renderer = GetComponent<GridCellRenderer>();
            Renderer.Init();
            _cellPresetType = preset;
            
            LineIndex = lineIndex;
            ColumnIndex = columnIndex;
            TeamIndex = teamIndex;

            Renderer.SetActive(CellPresetType.None);
        }
        
        public void AddUnit(Unit unit)
        {
            Unit = unit;
            Unit.SetTeam(TeamIndex);
            Renderer.SetActive(_cellPresetType);
            Renderer.SubscribeToUnit(unit);
            Renderer.Render(unit);
        }

        public void RemoveUnit()
        {
            if (Unit == null)
                return;
            
            Renderer.UnsubscribeFromUnit();
            Renderer.SetActive(CellPresetType.None);
            Renderer.Render(null);
            Unit = null;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var obj = eventData.pointerCurrentRaycast.gameObject;

            if (!obj.TryGetComponent(out GridCell cell))
            {
                DragOverCell?.Invoke(this, null);
                return;
            }
            if (_dragOverCell != null && _dragOverCell == cell) return;
            
            DragOverCell?.Invoke(this, cell);
            _dragOverCell = cell;
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

        public void OnPointerUp(PointerEventData eventData)
        {
            StopCoroutine(_holdRoutine);

            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out GridCell cell) && cell != this)
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
    }
}
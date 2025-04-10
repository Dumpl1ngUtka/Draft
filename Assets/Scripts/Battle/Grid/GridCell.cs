using System;
using Battle.DamageSystem;
using Battle.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Grid
{
    public class GridCell : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private float _holdTimer;
        private float _holdTime = 0.2f;
        private bool _isTimerFinished = true;
        private GridCell _dragOverCell;
        
        public int TeamIndex { get; private set; }
        public int LineIndex { get; private set; }
        public int ColumnIndex { get; private set; }
        public Unit Unit { get; private set; }
        public GridCellRenderer Renderer { get; private set; }
        
        public Action<GridCell, GridCell> DragOverCell;
        public Action<GridCell, GridCell> DragFinished;
        public Action<GridCell> Clicked;
        public Action<GridCell> HoldBegin;
        public Action<GridCell> HoldFinished;
        
        public void Init(int lineIndex, int columnIndex,int teamIndex)
        {
            Renderer = GetComponent<GridCellRenderer>();
            
            LineIndex = lineIndex;
            ColumnIndex = columnIndex;
            TeamIndex = teamIndex;

            Renderer.SetActive(false);
        }
        
        public void AddUnit(Unit unit)
        {
            Unit = unit;
            Unit.SetTeam(TeamIndex);
            Unit.ParametersChanged += ParametersChanged; 
            Renderer.SetActive(true);
            ParametersChanged();
        }

        private void Update()
        {
            if (_holdTimer > 0)
                _holdTimer -= Time.deltaTime;
            else if (!_isTimerFinished)
            {
                _isTimerFinished = true;
                HoldBegin?.Invoke(this);
            }
        }

        public void RemoveUnit()
        {
            if (Unit == null)
                return;
            
            Unit.ParametersChanged -= ParametersChanged;
            Renderer.SetActive(false);
            Unit = null;
        }

        private void ParametersChanged()
        {
            Renderer.Render(Unit);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //Clicked?.Invoke(this);
        }

        private void OnEnable()
        {
            if (Unit == null)
                return;
                
            Unit.ParametersChanged += ParametersChanged; 
        }
        
        private void OnDisable()
        {
            if (Unit == null)
                return;
            
            Unit.ParametersChanged -= ParametersChanged; 
        }

        public void OnEndDrag(PointerEventData eventData)
        {   
            /*var obj = eventData.pointerCurrentRaycast.gameObject;
            if (obj.TryGetComponent(out GridCell cell))
                DragFinished?.Invoke(this, cell);*/
        }

        public void OnDrag(PointerEventData eventData)
        {
            var obj = eventData.pointerCurrentRaycast.gameObject;
            
            if (!obj.TryGetComponent(out GridCell cell)) return;
            if (_dragOverCell != null && _dragOverCell == cell) return;
            
            DragOverCell?.Invoke(this, cell);
            _dragOverCell = cell;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isTimerFinished = false;
            _holdTimer = _holdTime;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _dragOverCell = null;
            var obj = eventData.pointerCurrentRaycast.gameObject;
            if (obj.TryGetComponent(out GridCell cell))
            {
                if (cell != this)
                    DragFinished?.Invoke(this, cell); 
                else if (_holdTimer > 0)
                    Clicked?.Invoke(this);
                else
                    HoldFinished?.Invoke(this); 
            }
            _holdTimer = 0f;
            _isTimerFinished = true;
        }
    }
}
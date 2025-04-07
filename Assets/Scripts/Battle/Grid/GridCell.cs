using System;
using Battle.DamageSystem;
using Battle.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Grid
{
    public class GridCell : MonoBehaviour, IPointerClickHandler, IEndDragHandler, IDragHandler
    {
        private GridCellRenderer _renderer;
        public int TeamIndex { get; private set; }
        public int LineIndex { get; private set; }
        public int ColumnIndex { get; private set; }
        public Unit Unit { get; private set; }

        public Action<GridCell, GridCell> Dragged;
        public Action<GridCell> Clicked;
        
        public void Init(int lineIndex, int columnIndex,int teamIndex)
        {
            _renderer = GetComponent<GridCellRenderer>();
            
            LineIndex = lineIndex;
            ColumnIndex = columnIndex;
            TeamIndex = teamIndex;

            _renderer.SetActive(false);
        }
        
        public void AddUnit(Unit unit)
        {
            Unit = unit;
            Unit.SetTeam(TeamIndex);
            Unit.ParametersChanged += ParametersChanged; 
            _renderer.SetActive(true);
            ParametersChanged();
        }

        public void RemoveUnit()
        {
            if (Unit == null)
                return;
            
            Unit.ParametersChanged -= ParametersChanged;
            _renderer.SetActive(false);
            Unit = null;
        }

        private void ParametersChanged()
        {
            _renderer.Render(Unit);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(this);
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
            var obj = eventData.pointerCurrentRaycast.gameObject;
            if (obj.TryGetComponent(out GridCell cell))
                Dragged?.Invoke(this, cell);
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }
}
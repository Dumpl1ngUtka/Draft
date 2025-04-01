using System;
using Battle.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Grid
{
    public class GridCell : MonoBehaviour, IPointerClickHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private GridCellRenderer _renderer;
        private Grid _grid;
        public RectTransform RectTransform => _rectTransform;
        public int Index { get; private set; }
        public int LineIndex { get; private set; }
        public Unit Unit { get; private set; }
        
        public Action<GridCell, GridCell> Dragged;
        public Action<GridCell> Clicked;

        public void Init(int index, int lineIndex, Grid grid)
        {
            Index = index;
            LineIndex = lineIndex;
            _grid = grid;
            _renderer.SetActive(false);
        }

        public void AddUnit(Unit unit)
        {
            Unit = unit;
            Unit.ParametersChanged += UnitParametersChanged; 
            _renderer.SetActive(true);
            UnitParametersChanged();
        }

        public void RemoveUnit()
        {
            if (Unit == null)
                return;
            
            Unit.ParametersChanged -= UnitParametersChanged;
            _renderer.SetActive(false);
            Unit = null;
        }

        private void UnitParametersChanged()
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
                
            Unit.ParametersChanged += UnitParametersChanged; 
        }
        
        private void OnDisable()
        {
            if (Unit == null)
                return;
            
            Unit.ParametersChanged -= UnitParametersChanged; 
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
using System;
using Battle.DamageSystem;
using Battle.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Grid
{
    public class GridCell : MonoBehaviour, IPointerClickHandler, IEndDragHandler, IDragHandler
    {
        private const int _maxPower = 6;
        
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private GridCellRenderer _renderer;
        private Grid _grid;
        public RectTransform RectTransform => _rectTransform;
        public int Index { get; private set; }
        public int LineIndex { get; private set; }
        public int ColumnIndex { get; private set; }
        public Unit Unit { get; private set; }
        public GridCellHealth Health { get; private set; }
        public int DicePower { get; private set; }
        public Action<GridCell, GridCell> Dragged;
        public Action<GridCell> Clicked;

        public bool IsUnitDead { get; private set; } = false;
        public bool IsUnitFinished = false;

        public void Init(int index, int lineIndex, int columnIndex, Grid grid)
        {
            Index = index;
            LineIndex = lineIndex;
            ColumnIndex = columnIndex;
            _grid = grid;
            
            _renderer.SetActive(false);
        }

        private void OnDead()
        {
            IsUnitDead = true;
            _renderer.SetActive(false);
        }

        public void AddUnit(Unit unit)
        {
            Unit = unit;
            Unit.ParametersChanged += UnitParametersChanged; 
            _renderer.SetActive(true);
            Health = new GridCellHealth();
            Health.OnDead += OnDead;
            Health.OnHealthChanged += _renderer.RenderHealth;
            Health.OnArmorChanged += _renderer.RenderArmor;
            Health.Init(unit);
            UnitParametersChanged();
        }

        public void RemoveUnit()
        {
            Health = new GridCellHealth();
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
        
        public void SetPower(int newPower)
        {
            if (newPower > _maxPower)
                newPower -= _maxPower;
            DicePower = newPower;
            UnitParametersChanged();
        }
    }
}
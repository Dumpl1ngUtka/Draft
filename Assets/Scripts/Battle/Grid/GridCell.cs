using System;
using Battle.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Grid
{
    public class GridCell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private DuckMeshController _duckMeshController;
        [SerializeField] private GridCellRenderer _renderer;
        private Grid _grid;
        public RectTransform RectTransform => _rectTransform;
        public int Index { get; private set; }
        public int LineIndex { get; private set; }
        public Unit Unit { get; private set; }
        
        public void Init(int index, int lineIndex, Grid grid)
        {
            Index = index;
            LineIndex = lineIndex;
            _grid = grid;
            _duckMeshController.SetVisibility(false);
        }

        public void AddUnit(Unit unit)
        {
            Unit = unit;
            unit.ParametersChanged += UnitParametersChanged; 
            _duckMeshController.SetVisibility(true);
            _duckMeshController.SetClothes();
        }

        private void UnitParametersChanged()
        {
            _renderer.Render(Unit);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Unit == null)
                _grid.ShowUnitSelectMenu(Index);
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
    }
}
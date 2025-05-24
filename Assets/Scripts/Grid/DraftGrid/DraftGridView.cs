using System.Collections.Generic;
using Battle.Grid.Visualization;
using Grid.Cells;
using Grid.DraftGrid.ChemistryBoard;
using Grid.GridEffects.UnitGridCellEffects;
using Units;
using UnityEngine;

namespace Grid.DraftGrid
{
    public class DraftGridView : GridView
    {
        [SerializeField] private SelectUnitsMenu.SelectUnitsPanel _selectUnitsPanel;
        [SerializeField] private ChemistryObserver _chemistryObserver;
        private List<Effect> _effects = new List<Effect>();
        private Effect _dragEffect;

        private void Awake()
        {
            HideSelectMenu();
            
        }

        public void InitChemistryObserver(CustomObserver.IObservable<ChemestryInteractor> observable)
        {
            _chemistryObserver.Init(observable);
        }

        public void ShowSelectMenu(List<Unit> units)
        {
            _selectUnitsPanel.SetActive(true);
            _selectUnitsPanel.RenderUnits(units);
        }
        
        public void HideSelectMenu()
        {
            _selectUnitsPanel.SetActive(false);
        }

        public void PlayDragEffect(UnitGridCell cell)
        {
            var renderer = cell.GetComponent<UnitGridCellRenderer>();
            var effect = new ShakeEffect(renderer, true, 1, 1, 1.2f, 10);
            _dragEffect = effect;
        }

        public void StopDragEffect()
        {
            _dragEffect?.StopEffect();
            _dragEffect = null;
        }
    }
}
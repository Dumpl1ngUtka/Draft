using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unit = Battle.Units.Unit;

namespace Battle.Grid.Visualization
{
    public class GridCellRenderer : MonoBehaviour
    {
        [Header("Interactors")]
        [SerializeField] private DuckIconInteractor _duckIconInteractor;
        [SerializeField] private ChemistryInteractor _chemistryInteractor;
        [SerializeField] private PassiveEffectsInteractor _passiveInteractor;
        [SerializeField] private ParametersInteractor _parameterInteractor;
        [SerializeField] private UnitBelongingInteractor _unitBelongingInteractor;
        [SerializeField] private HealthInteractor _healthInteractor;
        [SerializeField] private DiceInteractor _diceInteractor;
        [SerializeField] private OverTextInteractor _overTextInteractor;
        [SerializeField] private OverPanelInteractor _overPanelInteractor;
        [SerializeField] private SizeInteractor _sizeInteractor;
        private Unit _unit;
        private List<GridCellInteractor> _allInteractors;

        public void Init()
        {
            GroupInteractors();
        }
        
        private void GroupInteractors()
        {
            _allInteractors = new List<GridCellInteractor>
            {
                _duckIconInteractor,
                _parameterInteractor,
                _chemistryInteractor,
                _diceInteractor,
                _passiveInteractor,
                _healthInteractor,
                _overPanelInteractor,
                _overTextInteractor,
            };
        }
        
        public void SubscribeToUnit(Unit unit)
        {
            _unit = unit;
            _unit.ParametersChanged += ParametersChanged;
            _unit.ChemistryChanged += ChemistryChanged; 
            _unit.HealthChanged += HealthChanged; 
            _unit.DicePowerChanged += DicePowerChanged;  
            _unit.ReadyStatusChanged += ReadyStatusChanged;
            _unit.PassiveEffectsChanged += PassiveEffectsChanged;
            Render(_unit);
        }

        public void UnsubscribeFromUnit()
        {
            _unit.ParametersChanged -= ParametersChanged;
            _unit.ChemistryChanged -= ChemistryChanged; 
            _unit.HealthChanged -= HealthChanged; 
            _unit.DicePowerChanged -= DicePowerChanged;  
            _unit.ReadyStatusChanged -= ReadyStatusChanged;
            _unit.PassiveEffectsChanged -= PassiveEffectsChanged;
            _unit = null;
        }
        
        private void PassiveEffectsChanged() => _passiveInteractor.Render(_unit);
        
        private void ReadyStatusChanged() => _diceInteractor.Render(_unit);

        private void DicePowerChanged() => _diceInteractor.Render(_unit);

        private void HealthChanged() => _healthInteractor.Render(_unit);

        private void ChemistryChanged() => _chemistryInteractor.Render(_unit);

        private void ParametersChanged() => Render(_unit);

        public void SetOverText(string text)
        {
            _overTextInteractor.SetText(text);
        }

        public void SetOverPanel(Color color = default)
        {
            _overPanelInteractor.SetColor(color);
        }
        
        public void SetSize(float size, bool instantly = false)
        {
            if (instantly)
                _sizeInteractor.SetSizeInstantly(size);
            else
                _sizeInteractor.SetSize(size);
        }

        private void Update()
        {
            _sizeInteractor.Update();
        }

        public void SetActive(CellPresetType preset)
        {
            foreach (var interactor in _allInteractors)
                interactor.SetActive(preset);
        }
        
        public void Render(Unit unit)
        {
            if (unit == null || unit.IsDead)
            {
                SetActive(CellPresetType.None);
                return;
            }
            
            foreach (var interactor in _allInteractors)
                interactor.Render(unit);
        }

        public void RenderDiceAdditionValue(int value)
        {
            var isZero = value == 0;
            _diceInteractor.SetActiveAdditionalValue(!isZero);
            if (!isZero)
                _diceInteractor.RenderAdditionalValue(value);
        }
    }
}

using System.Collections.Generic;
using Battle.PassiveEffects;
using CustomObserver;
using Units;
using UnityEngine;
using Unit = Units.Unit;

namespace Battle.Grid.Visualization
{
    public class UnitGridCellRenderer : MonoBehaviour
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
            foreach (var interactor in _allInteractors) 
                interactor.Init(null);
            _overTextInteractor.SetText("");
        }
        
        private void GroupInteractors()
        {
            _allInteractors = new List<GridCellInteractor>
            {
                _duckIconInteractor,
                _chemistryInteractor,
                _passiveInteractor,
                _parameterInteractor,
                _unitBelongingInteractor,
                _healthInteractor,
                _diceInteractor,
                _overPanelInteractor,
                _overTextInteractor,
            };
        }
        
        public void SubscribeToUnit(Unit unit)
        {
            foreach (var interactor in _allInteractors)
            {
                interactor.Init(unit);
                interactor.TryUpdateInfo();
            }

            SubscrabeToUnitAction(unit);
            
            _unit = unit;
        }
        
        public void UnsubscribeFromUnit()
        {
            if (_unit == null)
                return;
            
            UnsubscrabeFromUnitAction();
            _unit = null;

            foreach (var interactor in _allInteractors)
            {
                interactor.Init(null);
                interactor.TryUpdateInfo();
            }
        }
        
        public void PlayAnimation(AnimationClip animationClip)
        {
            _overPanelInteractor.PlayAnimation(animationClip);
        }
        
        public void SetOverText(string text)
        {
            _overTextInteractor.SetText(text);
        }
        
        public void SetSize(float size, bool instantly = false)
        {
            if (instantly)
                _sizeInteractor.SetSizeInstantly(size);
            else
                _sizeInteractor.SetSize(size);
        }
        
        public void Render(Unit unit)
        {
            if (unit == null || unit.Stats.IsDead)
            {
                return;
            }
        }

        public void RenderDiceAdditionValue(int value)
        {
            _diceInteractor.RenderAdditionalValue(value);
        }

        public void UpdateObserver(UnitStats interactor)
        {
            _duckIconInteractor.TryUpdateInfo();
            _chemistryInteractor.TryUpdateInfo();
            _parameterInteractor.TryUpdateInfo();
            _healthInteractor.TryUpdateInfo();
            _diceInteractor.TryUpdateInfo();
        }

        public void SetSpriteColor(Color color)
        {
            _duckIconInteractor.SetSpriteColor(color);
        }
        
        private void Update()
        {
            _sizeInteractor.Update();
        }

        private void SubscrabeToUnitAction(Unit unit)
        {
            unit.PassiveEffectsHolder.EffectApplied += _overPanelInteractor.PlayEffectAnimation;
            unit.PassiveEffectsHolder.PassiveEffectsChanged += _passiveInteractor.TryUpdateInfo;
            
            unit.Stats.HealthChanged += _healthInteractor.TryUpdateInfo;
            unit.Stats.HealthChanged += _duckIconInteractor.TryUpdateInfo;
            unit.Stats.HealthChanged += _chemistryInteractor.TryUpdateInfo;
            
            unit.Stats.Energy.StatChanged += _diceInteractor.TryUpdateInfo;
            unit.DiceInteractor.DiceValueChanged += _diceInteractor.TryUpdateInfo;
            
            unit.Stats.AttributeChanged += _parameterInteractor.TryUpdateInfo;
        }

        private void UnsubscrabeFromUnitAction()
        {
            _unit.PassiveEffectsHolder.EffectApplied -= _overPanelInteractor.PlayEffectAnimation;
            _unit.PassiveEffectsHolder.PassiveEffectsChanged -= _passiveInteractor.TryUpdateInfo;
            
            _unit.Stats.HealthChanged -= _healthInteractor.TryUpdateInfo;
            _unit.Stats.HealthChanged -= _duckIconInteractor.TryUpdateInfo;
            _unit.Stats.HealthChanged -= _chemistryInteractor.TryUpdateInfo;
            
            _unit.Stats.Energy.StatChanged -= _diceInteractor.TryUpdateInfo;
            _unit.DiceInteractor.DiceValueChanged -= _diceInteractor.TryUpdateInfo;
            
            _unit.Stats.AttributeChanged -= _parameterInteractor.TryUpdateInfo;
        }
        
        private void OnDisable()
        {
            UnsubscribeFromUnit();
            _overPanelInteractor.OnDestroy();
        }
    }
}

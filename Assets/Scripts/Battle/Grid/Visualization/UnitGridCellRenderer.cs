using System.Collections.Generic;
using Battle.PassiveEffects;
using UnityEngine;
using Unit = Battle.Units.Unit;

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
            
            _overPanelInteractor.Init();
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
            _unit = unit;
            foreach (var interactor in _allInteractors) 
                interactor.Init(_unit);
            
            _unit.Stats.HealthChanged += () => _diceInteractor.UpdateInfo();
            _unit.Stats.HealthChanged += HealthChanged;
            _unit.ParametersChanged += ParametersChanged;
            _unit.ChemistryChanged += ChemistryChanged; 
            _unit.DicePowerChanged += DicePowerChanged;  
            _unit.ReadyStatusChanged += ReadyStatusChanged;
            _unit.PassiveEffectsChanged += PassiveEffectsChanged;
            _unit.PassiveEffectsHolder.EffectApplied += EffectApplied;
            //_unit.Stats.HealthChanged += HealthChanged;
            Render(_unit);
        }

        private void EffectApplied(PassiveEffect effect, TriggerType type)
        {
            var clip = effect.GetClipByType(type);
            if (clip != null)
            {
                PlayAnimation(clip, clip.length);
            }
            
        }

        public void UnsubscribeFromUnit()
        {
            _unit.ParametersChanged -= ParametersChanged;
            _unit.ChemistryChanged -= ChemistryChanged; 
            _unit.DicePowerChanged -= DicePowerChanged;  
            _unit.ReadyStatusChanged -= ReadyStatusChanged;
            _unit.PassiveEffectsChanged -= PassiveEffectsChanged;
            //_unit.Stats.HealthChanged -= HealthChanged;

            _unit = null;
        }
        
        private void PassiveEffectsChanged()
        {
            _passiveInteractor.UpdateInfo();
            _healthInteractor.UpdateInfo();
        }

        private void ReadyStatusChanged() => _diceInteractor.UpdateInfo();

        private void DicePowerChanged() => _diceInteractor.UpdateInfo();

        private void HealthChanged() => _healthInteractor.UpdateInfo();

        private void ChemistryChanged() => _chemistryInteractor.UpdateInfo();

        private void ParametersChanged() => Render(_unit);

        private void Update()
        {
            _sizeInteractor.Update();
            _overPanelInteractor.Update();
        }

        public void PlayAnimation(AnimationClip animationClip, float duration)
        {
            _overPanelInteractor.PlayOneShotAnimation(animationClip, duration);
        }
        
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
        
        public void Render(Unit unit)
        {
            if (unit == null || unit.IsDead)
            {
                return;
            }
        }

        public void RenderDiceAdditionValue(int value)
        {
            var isZero = value == 0;
            _diceInteractor.SetActiveAdditionalValue(!isZero);
            if (!isZero)
                _diceInteractor.RenderAdditionalValue(value);
        }

        private void OnDestroy()
        {
            _overPanelInteractor.OnDestroy();
        }
    }
}

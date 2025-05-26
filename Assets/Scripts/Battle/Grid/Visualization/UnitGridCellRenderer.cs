using System.Collections.Generic;
using Battle.PassiveEffects;
using CustomObserver;
using Units;
using UnityEngine;
using Unit = Units.Unit;

namespace Battle.Grid.Visualization
{
    public class UnitGridCellRenderer : MonoBehaviour, 
        IObserver<UnitStats>, IObserver<PassiveEffectsHolder>
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
                interactor.Init(_unit);
            _overPanelInteractor.Init();
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
            unit.Stats.AddObserver(this);

            foreach (var interactor in _allInteractors) 
                interactor.Init(unit);

            UpdateObserver(unit.Stats);
            
            _unit = unit;
        }
        
        public void UnsubscribeFromUnit()
        {
            _unit.Stats.RemoveObserver(this);
            
            _unit = null;
            
            foreach (var interactor in _allInteractors) 
                interactor.Init(null);
            
            UpdateObserver((UnitStats)null);
            UpdateObserver((PassiveEffectsHolder)null);
        }

        private void EffectApplied(PassiveEffect effect, TriggerType type)
        {
            var clip = effect.GetClipByType(type);
            if (clip != null)
            {
                PlayAnimation(clip, clip.length);
            }
        }

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
            if (unit == null || unit.Stats.IsDead)
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

        public void UpdateObserver(UnitStats interactor)
        {
            _duckIconInteractor.TryUpdateInfo();
            _chemistryInteractor.TryUpdateInfo();
            _parameterInteractor.TryUpdateInfo();
            _healthInteractor.TryUpdateInfo();
        }
        
        public void UpdateObserver(PassiveEffectsHolder interactor)
        {
            _passiveInteractor.TryUpdateInfo();
        }
    }
}

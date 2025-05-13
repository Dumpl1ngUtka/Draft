using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;

namespace Battle.PassiveEffects
{
    public class PassiveEffectsHolder
    {
        private List<PassiveEffect> _passiveEffects;
        
        public Action PassiveEffectsChanged;
        public Action<PassiveEffect, TriggerType> EffectApplied;
        
        public PassiveEffectsHolder(Unit unit)
        {
            unit.TurnEnded += OnTurnEnded;
            _passiveEffects = new List<PassiveEffect>();
        }
        
        public void AddEffect(PassiveEffect effect)
        {
            var newEffect = CheckCombinationEffects(effect);
            newEffect.OnAdd();
            EffectApplied?.Invoke(newEffect, TriggerType.Add);
            if (newEffect.TurnCount == 0)
            {
                newEffect.Destroy();
                EffectApplied?.Invoke(newEffect, TriggerType.Destroy);
            }
            else
            {
                _passiveEffects.Add(newEffect);
                PassiveEffectsChanged?.Invoke();
            }
        }

        private PassiveEffect CheckCombinationEffects(PassiveEffect addedEffect)
        {
            foreach (var passiveEffect in _passiveEffects.ToList())
            {
                if (passiveEffect is not ICombinationEffect effect) continue;
                
                var newEffect = effect.GetCombinationEffect(addedEffect);
                
                _passiveEffects.Remove(passiveEffect);
                return newEffect;
            }
            return addedEffect;
        }

        private void OnTurnEnded()
        {
            foreach (var effect in _passiveEffects)
            {
                effect.OnTurnEnded();
                EffectApplied?.Invoke(effect, TriggerType.TurnEnded);
                effect.ReduceCount();
                if (effect.TurnCount > 0) continue;
                
                effect.Destroy();
                EffectApplied?.Invoke(effect, TriggerType.Destroy);
            }  
            _passiveEffects = _passiveEffects.Where(effect => effect != null).ToList();
            PassiveEffectsChanged?.Invoke();
        }

        public List<PassiveEffect> GetPassiveEffects()
        {
            return _passiveEffects;
        }
    }
}
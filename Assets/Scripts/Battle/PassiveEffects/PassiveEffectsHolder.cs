using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.PassiveEffects
{
    public class PassiveEffectsHolder
    {
        private List<PassiveEffect> _passiveEffects = new();
        
        public Action PassiveEffectsChanged;
        public Action<PassiveEffect, TriggerType> EffectApplied;

        public PassiveEffectsHolder(PassiveEffect[] startPassiveEffects)
        {
            if (startPassiveEffects == null || startPassiveEffects.Length == 0)
                return;
            
            foreach (var passiveEffect in startPassiveEffects) 
                AddEffect(passiveEffect);
        }

        public PassiveEffectsHolder()
        {
        }
        
        public void AddEffect(PassiveEffect additionalEffect)
        {
            if (additionalEffect == null)
                return;

            var combinationResult = new CombinationResult(false);

            foreach (var availableEffect in _passiveEffects)
            {
                var result = PassiveEffectsCombination.Check(availableEffect, additionalEffect);
                
                if (!result.IsCombined)
                    continue;
                
                if (result.DestoyAvailableEffect)
                    RemoveEffect(additionalEffect);
                
                combinationResult = result;
                break;
            }
            
            if (combinationResult.AddNewEffect)
            {
                additionalEffect.OnAdd();
                EffectApplied?.Invoke(additionalEffect, TriggerType.Add);
                if (additionalEffect.TurnCount == 0)
                {
                    RemoveEffect(additionalEffect);
                }
                else
                {
                    _passiveEffects.Add(additionalEffect);
                    PassiveEffectsChanged?.Invoke();
                }
            }

            AddEffect(combinationResult.CombinedEffect);
        }
        
        public void OnTurnEnded()
        {
            foreach (var effect in _passiveEffects)
            {
                effect.OnTurnEnded();
                EffectApplied?.Invoke(effect, TriggerType.TurnEnded);
                effect.ReduceCount();
                if (effect.TurnCount > 0) continue;

                RemoveEffect(effect);
            }  
            _passiveEffects = _passiveEffects.Where(effect => effect.TurnCount > 0).ToList();
            PassiveEffectsChanged?.Invoke();
        }

        public List<PassiveEffect> GetPassiveEffects() => _passiveEffects;

        public void RemoveEffect(PassiveEffect effect)
        {
            effect.Destroy();
            EffectApplied?.Invoke(effect, TriggerType.Destroy);
        }
    }
}
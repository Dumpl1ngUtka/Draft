using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle.PassiveEffects
{
    public class PassiveEffectsHolder : CustomObserver.IObservable<PassiveEffectsHolder>
    {
        private List<PassiveEffect> _passiveEffects = new();
        
        public Action PassiveEffectsChanged;
        public Action<PassiveEffect, TriggerType> EffectApplied;

        public void AddEffect(PassiveEffect effect)
        {
            if (effect == null)
                return;
            
            effect.OnAdd();
            EffectApplied?.Invoke(effect, TriggerType.Add);
            if (effect.TurnCount == 0)
            {
                effect.Destroy();
                EffectApplied?.Invoke(effect, TriggerType.Destroy);
            }
            else
            {
                _passiveEffects.Add(effect);
                PassiveEffectsChanged?.Invoke();
            }
        }
        
        public void OnTurnEnded()
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
            _passiveEffects = _passiveEffects.Where(effect => effect.TurnCount > 0).ToList();
            PassiveEffectsChanged?.Invoke();
        }

        public List<PassiveEffect> GetPassiveEffects() => _passiveEffects;

        #region Observers

        private List<CustomObserver.IObserver<PassiveEffectsHolder>> _observers = new();
        
        public void AddObserver(CustomObserver.IObserver<PassiveEffectsHolder> observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(CustomObserver.IObserver<PassiveEffectsHolder> observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
                observer.UpdateObserver(this);
        }

        #endregion
    }
}
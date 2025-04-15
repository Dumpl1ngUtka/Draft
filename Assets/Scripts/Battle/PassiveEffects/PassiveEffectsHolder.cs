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
        
        public PassiveEffectsHolder(Unit unit)
        {
            unit.TurnEnded += OnTurnEnded;
            _passiveEffects = new List<PassiveEffect>();
        }
        
        public void AddEffect(PassiveEffect effect)
        {
            effect.OnAdd();
            if (effect.TurnCount == 0)
            {
                effect.Destroy();
            }
            else
            {
                _passiveEffects.Add(effect);
                PassiveEffectsChanged?.Invoke();
            }
        }

        private void OnTurnEnded()
        {
            foreach (var effect in _passiveEffects)
            {
                effect.OnTurnEnded();
                effect.ReduceCount();
                if (effect.TurnCount <= 0)
                    effect.Destroy();
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
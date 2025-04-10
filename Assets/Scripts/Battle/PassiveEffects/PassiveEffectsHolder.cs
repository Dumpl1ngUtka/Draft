using System.Collections.Generic;
using Battle.Units;
using UnityEngine;

namespace Battle.PassiveEffects
{
    public class PassiveEffectsHolder
    {
        private readonly Unit _unit;
        private readonly List<PassiveEffect> _passiveEffects;
        
        public PassiveEffectsHolder(Unit unit)
        {
            _unit = unit;
            _unit.TurnEnded += OnTurnEnded;
            _passiveEffects = new List<PassiveEffect>();
        }
        
        public void AddEffect(PassiveEffect effect)
        {
            effect.OnAdd();
            if (effect.TurnCount == 0)
                effect.Destroy();
            else
                _passiveEffects.Add(effect);
        }

        private void OnTurnEnded()
        {
            foreach (var effect in _passiveEffects)
            {
                effect.OnTurnEnded();
                effect.ReduceCount();
                if (effect.TurnCount == 0)
                    effect.Destroy();
            }  
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Battle.PassiveEffects;

namespace Battle.Units
{
    public class Stat
    {
        private float _baseValue;
        private List<StatModifier> _modifier = new List<StatModifier>();
        
        public float Value => _modifier.Aggregate(_baseValue, (current, modifier) => modifier(current));

        public Stat(float baseValue) => _baseValue = baseValue;
        
        public void AddFunc(StatModifier func) => _modifier.Add(func);

        public void RemoveFunc(StatModifier func) => _modifier.Remove(func);
    }
}
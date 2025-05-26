using System.Collections.Generic;
using System.Linq;

namespace Units
{
    public class Stat
    {
        private float _baseValue;
        private List<StatModifierFunction> _modifier = new List<StatModifierFunction>();
        
        public float Value => _modifier.Aggregate(_baseValue, (current, modifier) => modifier(current));

        public Stat(float baseValue) => _baseValue = baseValue;
        
        public void AddFunc(StatModifierFunction func) => _modifier.Add(func);

        public void RemoveFunc(StatModifierFunction func) => _modifier.Remove(func);
    }
}
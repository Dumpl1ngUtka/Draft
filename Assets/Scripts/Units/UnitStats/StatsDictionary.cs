using System;
using System.Collections.Generic;

namespace Units
{
    public class StatsDictionary<T> where T : Enum
    {
        private readonly Dictionary<T, StatInt> _stats;

        public StatsDictionary()
        {
            _stats = new Dictionary<T, StatInt>();
            foreach (T key in typeof(T).GetEnumValues()) 
                _stats.Add(key, new StatInt(0));
        }

        public StatsDictionary<T> WithModifier(StatModifier modifier)
        {
            foreach (var stat in _stats) 
                stat.Value.AddModifier(modifier);
            return this;
        }

        public StatInt GetStatBy(T key) => _stats[key];
    }
}
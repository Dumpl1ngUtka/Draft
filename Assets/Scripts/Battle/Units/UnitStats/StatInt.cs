using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.Units
{
    public class StatInt
    {
        private readonly bool _isCanBeNegative;
        
        private int _baseValue;
        private List<StatModifier> _modifier = new List<StatModifier>();
        private int _additionalValue;
        
        public int Value
        {
            get
            {
                var result = _modifier.Aggregate(_baseValue, (current, modifier) => (int)modifier(current));
                 
                if (!_isCanBeNegative && result < 0)
                    result = 0;
                return result;
            }
        }

        public StatInt(int baseValue, bool isCanBeNegative = false)
        {
            _baseValue = baseValue;
            _isCanBeNegative = isCanBeNegative;
        }

        public void AddFunc(StatModifier func)
        {
            _modifier.Add(func);
        }

        public void RemoveFunc(StatModifier func)
        {
             _modifier.Remove(func);
        }

        public void AddValue(int value)
        {
            if (value < 0)
                throw new Exception("Value cannot be negative. Use RemoveValue() instead.");
            
            //_additionValue += value;
        }

        public void RemoveValue(int value)
        {
            if (value < 0)
                throw new Exception("Value cannot be negative. Use AddValue() instead.");
            
            //_additionValue -= value;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Units
{
    public class StatInt
    {
        private readonly int _baseValue;
        private readonly List<StatModifier> _modifiers;
        private readonly bool _isPositiveOrZero;
        private int _permanentAdditionModifier;
        private bool _isNeedToUpdate;
        private int _currentValue;
        
        public int Value
        {
            get
            {
                if (_isNeedToUpdate)
                {
                    _currentValue = GetUpdatedResult();
                    _isNeedToUpdate = false;
                }

                return _currentValue;
            }
        }

        public Action StatChanged;
        
        public StatInt(int baseValue, bool isPositiveOrZero = false)
        {
            _baseValue = baseValue;
            _modifiers = new List<StatModifier>();
            _isPositiveOrZero = isPositiveOrZero;
            _isNeedToUpdate = true;
        }

        public void AddModifier(StatModifier modifier)
        {
            if (modifier.Type == (int)StatModifierType.PermanentAdditiveValue)
            {
                _permanentAdditionModifier += (int)modifier.Function(0);
                return;
            }
            
            for (int i = 0; i < _modifiers.Count; i++)
            {
                if (_modifiers[i].Type > modifier.Type)
                {
                    _modifiers.Insert(i, modifier);
                    return;
                }
            }
            _modifiers.Add(modifier);
            CurrentValueOutdated();
            StatChanged?.Invoke();
        }
        
        public void AddModifier(PermanentStatModifier modifier)
        {
            _permanentAdditionModifier += (int)modifier.Function(0);
            CurrentValueOutdated();
            StatChanged?.Invoke();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            _modifiers.Remove(modifier);
            CurrentValueOutdated();
            StatChanged?.Invoke();
        }

        public void CurrentValueOutdated()
        {
            _isNeedToUpdate = true;
        }

        private int GetUpdatedResult()
        {
            var result = _baseValue;
            var index = 0;
            if (_modifiers.Count > 0)
            {
                var modifier = _modifiers[0];
                while (modifier is { Type: < (int)StatModifierType.PermanentAdditiveValue })
                {
                    result = (int)modifier.Function(result);
                    index++;
                    try {modifier = _modifiers[index]; }
                    catch { break; }
                }
            }
            if (_isPositiveOrZero)
                BalancePermanentAdditionModifier(result);
            result += _permanentAdditionModifier;
            for (var i = index; i < _modifiers.Count; i++)
            {
                result = (int)_modifiers[i].Function(result);
            }
            return result;
        }

        private void BalancePermanentAdditionModifier(int currentResult)
        {
            _permanentAdditionModifier = currentResult switch
            {
                >= 0 when _permanentAdditionModifier < 0 => Math.Max(-currentResult, _permanentAdditionModifier),
                < 0 when _permanentAdditionModifier > 0 => Math.Max(0, _permanentAdditionModifier),
                _ => _permanentAdditionModifier
            };
        }
    }
}
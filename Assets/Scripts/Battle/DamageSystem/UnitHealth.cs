using System;
using Unit = Battle.Units.Unit;

namespace Battle.DamageSystem
{
    public class UnitHealth
    {
        private const int _healthPointsPerHealthAttribute = 3;
        
        private int _currentHealth;
        private int _armorValue;
        private int _maxHealth;
        private Unit _unit;
        
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public int ArmorValue => _armorValue;
        
        public Action OnValueChanged;
        public Action OnDead;

        public UnitHealth(Unit unit)
        {
            _unit = unit;
            _armorValue = 0;
            _maxHealth = unit.Attributes.Health * _healthPointsPerHealthAttribute;
            _currentHealth = _maxHealth;
        }

        public void AddArmor(int value)
        {
            if (value < 0)
                return;
            
            _armorValue += value;
            OnValueChanged?.Invoke();
        }
        
        public void ApplyDamage(Damage damage)
        {
            var value = CalculateResistance(damage);
            value = CalculateArmor(value);
            _currentHealth -= value;
            if (_currentHealth < 0)
            {
                _currentHealth = 0;
                OnDead?.Invoke();
            }
            OnValueChanged?.Invoke();
        }

        private int CalculateArmor(int value)
        {
            if (_armorValue >= value)
            {
                _armorValue -= value;
                return 0; 
            }

            _armorValue = 0;
            OnValueChanged?.Invoke();
            return value - _armorValue;
        }
        
        private int CalculateResistance(Damage damage)
        {
            if (_unit.Immunities.Contains(damage.DamageType))
                return 0;
            if (_unit.Resistances.Contains(damage.DamageType))
                return damage.Value / 2;
            if (_unit.Vulnerability.Contains(damage.DamageType))
                return damage.Value * 2;
            return damage.Value;
        }
    }
}
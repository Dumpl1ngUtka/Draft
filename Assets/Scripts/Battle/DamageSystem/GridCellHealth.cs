using System;
using Unit = Battle.Units.Unit;

namespace Battle.DamageSystem
{
    public class GridCellHealth
    {
        private const int _healthPointsPerHealthAttribute = 3;
        
        private int _currentHealth;
        private int _armorValue;
        private int _maxHealth;
        private Unit _unit;
        
        public Action<int, int> OnHealthChanged;
        public Action<int> OnArmorChanged;
        public Action OnDead;

        public void Init(Unit unit)
        {
            _unit = unit;
            _armorValue = 0;
            _maxHealth = unit.Attributes.Health * _healthPointsPerHealthAttribute;
            _currentHealth = _maxHealth;
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
            OnArmorChanged?.Invoke(_armorValue);
        }

        public void AddArmor(int value)
        {
            if (value < 0)
                return;
            
            _armorValue += value;
            OnArmorChanged?.Invoke(_armorValue);
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
            OnHealthChanged?.Invoke(_currentHealth, _currentHealth);
        }

        private int CalculateArmor(int value)
        {
            if (_armorValue >= value)
            {
                _armorValue -= value;
                return 0; 
            }

            _armorValue = 0;
            OnArmorChanged?.Invoke(_armorValue);
            return value - _armorValue;
        }
        
        private int CalculateResistance(Damage damage)
        {
            if (_unit.Race.Immunities.Contains(damage.DamageType))
                return 0;
            if (_unit.Race.Resistances.Contains(damage.DamageType))
                return damage.Value / 2;
            if (_unit.Race.Vulnerability.Contains(damage.DamageType))
                return damage.Value * 2;
            return damage.Value;
        }
    }
}
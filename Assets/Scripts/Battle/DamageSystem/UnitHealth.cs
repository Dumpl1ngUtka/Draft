using System;
using Battle.Units;
using Units;
using Unit = Units.Unit;

namespace Battle.DamageSystem
{
    public class UnitHealth
    {
        private UnitStats _unitStats;

        public UnitHealth(Unit unit)
        {
            _unitStats = unit.Stats;
        }
        
        public void ApplyDamage(Damage damage, StatInt armor, StatInt currentHealth)
        {
            var damageValue = CalculateResistance(damage);
            damageValue = CalculateDamageAfterArmor(damageValue, armor);
            var damageModifier = new PermanentStatModifier(-damageValue);
            currentHealth.AddModifier(damageModifier);
        }

        private int CalculateDamageAfterArmor(int damageValue, StatInt armor)
        {
            var armorValue = armor.Value;
            
            var additionalDamage = Math.Min(armorValue, damageValue);
            var damageModifier = new PermanentStatModifier(-additionalDamage);
            armor.AddModifier(damageModifier);

            return Math.Min(damageValue - armorValue, 0);
        }
        
        private int CalculateResistance(Damage damage)
        {
            if (_unitStats.Immunities.Contains(damage.DamageType))
                return 0;
            if (_unitStats.Resistances.Contains(damage.DamageType))
                return damage.Value / 2;
            if (_unitStats.Vulnerability.Contains(damage.DamageType))
                return damage.Value * 2;
            return damage.Value;
        }
    }
}
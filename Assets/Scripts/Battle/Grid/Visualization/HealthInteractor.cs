using System;
using System.Linq;
using Battle.DamageSystem;
using Battle.Grid.CardParameter;
using Battle.PassiveEffects;
using Battle.Units;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class HealthInteractor : GridCellInteractor
    {
        [SerializeField] private ValueParameter _health;
        [SerializeField] private ValueParameter _armor;

        protected override void ActiveChanged(bool isActive)
        {
            _health?.gameObject.SetActive(isActive);
            _armor?.gameObject.SetActive(isActive);
        }

        protected override void UpdateInfo(Unit unit)
        {
            var unitHealth = unit.Health;
            _health?.Render(unitHealth.CurrentHealth, unitHealth.MaxHealth);
            var armor = unit.PassiveEffectsHolder.GetPassiveEffects().Sum(x => ((AddArmor)x).Value);
            _armor?.Render(armor);
        }
    }
}
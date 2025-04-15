using System;
using Battle.DamageSystem;
using Battle.Grid.CardParameter;
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
            _health.gameObject.SetActive(isActive);
            _armor.gameObject.SetActive(isActive);
        }

        protected override void UpdateInfo(Unit unit)
        {
            var unitHealth = unit.Health;
            _health.Render(unitHealth.CurrentHealth, unitHealth.MaxHealth);
            _armor.Render(unitHealth.ArmorValue);
        }
    }
}
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
            _health?.Render(unit.Stats.CurrentHealth, unit.Stats.MaxHealth.Value);
            _armor?.Render(unit.Stats.Armor.Value);
        }
    }
}
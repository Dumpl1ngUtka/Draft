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
        
        public override void UpdateInfo()
        {
            base.UpdateInfo();
            _health?.Render(Unit.Stats.CurrentHealth.Value, Unit.Stats.MaxHealth.Value);
            _armor?.Render(Unit.Stats.Armor.Value);
        }
    }
}
using System;
using Battle.Grid.CardParameter;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class HealthInteractor : GridCellInteractor
    {
        [SerializeField] private ValueParameter _health;
        [SerializeField] private ValueParameter _armor;
        
        protected override void UpdateInfo()
        {
            _health?.Render(Unit.Stats.CurrentHealth.Value, Unit.Stats.MaxHealth.Value);
            _armor?.Render(Unit.Stats.Armor.Value);
        }

        protected override void SetActive(bool isActive)
        {
            _health?.gameObject.SetActive(isActive);
            _armor?.gameObject.SetActive(isActive);
        }
    }
}
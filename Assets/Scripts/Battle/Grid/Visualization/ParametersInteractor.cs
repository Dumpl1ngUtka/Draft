using System;
using Battle.Grid.CardParameter;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class ParametersInteractor : GridCellInteractor
    {
        [SerializeField] private CircleParameter _health;
        [SerializeField] private CircleParameter _strength;
        [SerializeField] private CircleParameter _dexterity;
        [SerializeField] private CircleParameter _intelligence;

        protected override void UpdateInfo()
        {
            var unitStats = Unit.Stats;
            _health?.Render(unitStats.HealthAttribute.Value);
            _strength?.Render(unitStats.StrengthAttribute.Value);
            _dexterity?.Render(unitStats.DexterityAttribute.Value);
            _intelligence?.Render(unitStats.IntelligenceAttribute.Value);
        }

        protected override void SetActive(bool isActive)
        {
            _health?.gameObject.SetActive(isActive);
            _strength?.gameObject.SetActive(isActive);
            _dexterity?.gameObject.SetActive(isActive);
            _intelligence?.gameObject.SetActive(isActive);
        }
    }
}
using System;
using Battle.Grid.CardParameter;
using Battle.Units;
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

        public override void UpdateInfo()
        {
            base.UpdateInfo();
            var unitStats = Unit.Stats;
            _health?.Render(unitStats.HealthAttribute.Value);
            _strength?.Render(unitStats.StrengthAttribute.Value);
            _dexterity?.Render(unitStats.DexterityAttribute.Value);
            _intelligence?.Render(unitStats.IntelligenceAttribute.Value);
        }
    }
}
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
        
        protected override void ActiveChanged(bool isActive)
        {
            _health?.gameObject.SetActive(isActive);
            _strength?.gameObject.SetActive(isActive);
            _dexterity?.gameObject.SetActive(isActive);
            _intelligence?.gameObject.SetActive(isActive);
        }

        protected override void UpdateInfo(Unit unit)
        {
            var attributes = unit.Attributes;
            _health?.Render(attributes.Health);
            _strength?.Render(attributes.Strength);
            _dexterity?.Render(attributes.Dexterity);
            _intelligence?.Render(attributes.Intelligence);
        }
    }
}
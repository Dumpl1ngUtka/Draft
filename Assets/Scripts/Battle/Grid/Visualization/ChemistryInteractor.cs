using System;
using Battle.Grid.CardParameter;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class ChemistryInteractor : GridCellInteractor
    {
        [SerializeField] private CircleParameter _chemistry;

        protected override void UpdateInfo()
        {
            _chemistry?.Render(Unit.Stats.Chemistry.Value);
        }

        protected override void SetActive(bool isActive)
        {
            _chemistry?.gameObject.SetActive(isActive);
        }
    }
}
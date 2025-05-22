using System;
using Battle.Grid.CardParameter;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class ChemistryInteractor : GridCellInteractor
    {
        [SerializeField] private CircleParameter _chemistry;

        public override void UpdateInfo()
        {
            base.UpdateInfo();
            _chemistry?.Render(Unit.Chemistry);
        }
    }
}
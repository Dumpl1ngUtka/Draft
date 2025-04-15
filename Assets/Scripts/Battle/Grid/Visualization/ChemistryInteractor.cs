using System;
using Battle.Grid.CardParameter;
using Battle.Units;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class ChemistryInteractor : GridCellInteractor
    {
        [SerializeField] private CircleParameter _chemistry;
                            
        protected override void ActiveChanged(bool isActive)
        {
            _chemistry.gameObject.SetActive(isActive);
        }

        protected override void UpdateInfo(Unit unit)
        {
            _chemistry.Render(unit.Chemistry);
        }
    }
}
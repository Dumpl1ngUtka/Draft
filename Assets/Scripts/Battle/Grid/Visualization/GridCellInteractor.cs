using System;
using Battle.Units;
using Units;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    public abstract class GridCellInteractor 
    {
        protected Unit Unit;
        
        public void Init(Unit unit)
        {
            Unit = unit;
            SetActive(unit != null);
        }

        public void TryUpdateInfo()
        {
            if (Unit != null)
                UpdateInfo();
        }
        
        protected abstract void UpdateInfo();
        
        protected abstract void SetActive(bool isActive); 
    }
}
using System;
using Battle.Units;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    public abstract class GridCellInteractor 
    {
        protected Unit Unit;
        
        public void Init(Unit unit)
        {
            Unit = unit;
        }

        public virtual void UpdateInfo()
        {
            if (Unit == null) return;
        }
    }
}
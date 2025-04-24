using System;
using System.Collections.Generic;
using System.Linq;
using Grid.Cells;
using UnityEngine;
using UnityEngine.Serialization;

namespace Grid
{
    [Serializable]
    public struct UnitGridCellContainer
    {
        public Transform Container;
        public TeamType TeamType;

        public List<UnitGridCell> GetCells()
        {
            return Container.GetComponentsInChildren<UnitGridCell>().ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class CellPreset
    {
        [SerializeField] private List<GridType> types;

        public bool Include(GridType type)
        {
            if (types.Contains(GridType.All))
                return true;
            if (types.Contains(GridType.None))
                return false;
            return types.Contains(type);
        }
    }

    public enum GridType
    {
        All,
        Draft,
        Battle,
        None
    }
}
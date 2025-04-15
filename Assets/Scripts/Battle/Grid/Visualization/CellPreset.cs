using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class CellPreset
    {
        [SerializeField] private List<CellPresetType> types;

        public bool Include(CellPresetType type)
        {
            if (types.Contains(CellPresetType.All))
                return true;
            if (types.Contains(CellPresetType.None))
                return false;
            return types.Contains(type);
        }
    }

    public enum CellPresetType
    {
        All,
        Draft,
        Battle,
        None
    }
}
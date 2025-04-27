using System.Collections.Generic;
using Battle.Units;
using UnityEngine;

namespace PathMap
{
    public struct PathCellInfo
    {
        [SerializeField] private List<UnitPreset> _presets;
        
        public List<UnitPreset> Presets => _presets;
    }
}
using System;
using System.Collections.Generic;
using Battle.Units;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Config/EnemyPositionPreset")]
    public class EnemyPositionPreset : ScriptableObject
    {
        [SerializeField] private LinePreset _frontLinePreset;
        [SerializeField] private LinePreset _midleLinePreset;
        [SerializeField] private LinePreset _backLinePreset;

        public List<UnitPreset> GetUnitPresets()
        {
            var unitPresets = new List<UnitPreset>();
            unitPresets.AddRange(_frontLinePreset.GetUnitPresets());
            unitPresets.AddRange(_midleLinePreset.GetUnitPresets());
            unitPresets.AddRange(_backLinePreset.GetUnitPresets());
            return unitPresets;
        }
    }
    [Serializable]
    public struct LinePreset
    {
        public UnitPreset LeftPreset;
        public UnitPreset MidlePreset;
        public UnitPreset RightPreset;
        
        public List<UnitPreset> GetUnitPresets()
        {
            return new List<UnitPreset>
            {
                LeftPreset,
                MidlePreset,
                RightPreset
            };
        }
    }    
}
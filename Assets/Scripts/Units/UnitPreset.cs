using System.Collections.Generic;
using Abilities;
using Battle.DamageSystem;
using Battle.UseCardReactions;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Config/Units/UnitPreset")]
    public class UnitPreset : ScriptableObject
    {
        private const int _startAttributePoints = 20;
        
        public string Name;
        public Sprite Icon;
        public Race Race;
        public Class Class;
        public Covenant Covenant;
        public Attributes Attributes;
        public Ability[] Abilities;
        public Reaction Reaction;

        public static UnitPreset GeneratePreset(Class unitClass)
        {
            var unitPreset = ScriptableObject.CreateInstance<UnitPreset>();
            unitPreset.Class = unitClass; 
            unitPreset.Icon = unitClass.Icon;
            unitPreset.Race = unitClass.Races[Random.Range(0, unitClass.Races.Length)];
            unitPreset.Reaction = unitPreset.Race.Reaction;
            unitPreset.Name = unitPreset.Race.AvailableNames[Random.Range(0, unitPreset.Race.AvailableNames.Length)];
            unitPreset.Covenant = unitPreset.Race.AvailableCovenants[Random.Range(0, unitPreset.Race.AvailableCovenants.Length)];
            unitPreset.Attributes = new Attributes(_startAttributePoints);
            unitPreset.Abilities = unitClass.Abilities;
            return unitPreset;
        }

        public static Unit GenerateUnit(Class unitClass)
        {
            return new Unit(GeneratePreset(unitClass));
        }
    }
}
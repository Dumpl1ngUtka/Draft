using System;
using Abilities;
using Battle.UseCardReactions;
using UnityEngine;
using Random = System.Random;

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

        public static UnitPreset GeneratePreset(Class unitClass, Race race, Covenant covenant)
        {
            var random = new Random();
            var attributes = new Attributes(_startAttributePoints, random);
            return GeneratePreset(unitClass, race, covenant, attributes);
        }

        public static UnitPreset GeneratePreset(Class unitClass, Race race, Covenant covenant, Attributes attributes)
        {
            var unitPreset = ScriptableObject.CreateInstance<UnitPreset>();
            unitPreset.Class = unitClass; 
            unitPreset.Race = race;
            unitPreset.Covenant = covenant;
            unitPreset.Attributes = attributes;
            
            unitPreset.Icon = unitClass.Icon;
            unitPreset.Reaction = unitPreset.Race.Reaction;
            unitPreset.Name = unitPreset.Race.AvailableNames[0];
            unitPreset.Abilities = unitClass.Abilities;
            
            return unitPreset;
        }
    }
}
using System;
using System.Linq;
using Abilities;
using Battle.PassiveEffects;
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
        [HideInInspector] public Reaction Reaction;
        public PassiveEffect[] PassiveEffects;

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
            unitPreset.Reaction = unitPreset.Covenant.Reaction;
            unitPreset.Name = unitPreset.Race.AvailableNames[0];
            unitPreset.Abilities = unitClass.Abilities;

            return unitPreset;
        }

        public PassiveEffect[] GetPassiveEffects(Unit unit)
        {
            if (PassiveEffects != null)
                return PassiveEffects.Select(x => x.GetInstance(unit.Stats, unit.Stats, 1000)).ToArray();

            if (Race != null && Race.GetPassiveEffects(unit) != null)
                return Race.GetPassiveEffects(unit);

            return Array.Empty<PassiveEffect>();
        }
    }
}
    
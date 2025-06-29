using System;
using System.Collections.Generic;
using System.Linq;
using Battle.PassiveEffects;
using Battle.UseCardReactions;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "NewRace", menuName = "Config/Units/Race")]
    public class Race : ScriptableObject
    {
        private const string Path = "Configs/Units/Races/";
        
        public string Name;
        public Sprite Icon;
        public string[] AvailableNames;
        public PassiveEffect[] RaceEffects;

        public static Race GetObjectByName(string name)
        {
            var races = Resources.LoadAll<Race>(Path);
            return races.FirstOrDefault(race => race.Name == name);
        }
        
        public static IEnumerable<Race> GetObjectsByNames(string[] names)
        {
            var allRaces = Resources.LoadAll<Race>(Path);
            return allRaces.Where(race => names.Contains(race.Name));
        }

        public PassiveEffect[] GetPassiveEffects(Unit unit)
        { 
            if (RaceEffects == null || RaceEffects.Length == 0)
                return Array.Empty<PassiveEffect>();
            
            return RaceEffects.Select(effect => 
                effect.GetInstance(unit.Stats, unit.Stats, 1000)).ToArray();
        }
    }
}
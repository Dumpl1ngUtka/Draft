using System.Collections.Generic;
using System.Linq;
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
        public Covenant[] AvailableCovenants;
        public Reaction Reaction;

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
    }
}
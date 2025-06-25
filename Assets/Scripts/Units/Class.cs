using System.Collections.Generic;
using System.Linq;
using Abilities;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu (fileName = "Class", menuName = "Config/Units/Class")]
    public class Class : ScriptableObject
    {
        private const string Path = "Configs/Units/Classes/";
        
        public Sprite Icon;
        public string Name;
        public int[] LineIndexes;
        public Race[] Races;
        public Ability[] Abilities;

        public static Class GetObjectByName(string name)
        {
            var races = Resources.LoadAll<Class>(Path);
            return races.FirstOrDefault(race => race.Name == name);
        }
        
        public static IEnumerable<Class> GetObjectsByNames(string[] names)
        {
            var allRaces = Resources.LoadAll<Class>(Path);
            return allRaces.Where(race => names.Contains(race.Name));
        }
    }
}
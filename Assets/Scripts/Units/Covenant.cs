using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "NEw covenant", menuName = "Config/Units/Covenant")]
    public class Covenant : ScriptableObject
    {
        private const string Path = "Configs/Units/Covenants/";
        
        public string ShortName;
        public string Name;
        public Sprite Icon;
        public CovenantType Type;
        
        public static Covenant GetObjectByName(string name)
        {
            var covenants = Resources.LoadAll<Covenant>(Path);
            return covenants.FirstOrDefault(covenant => covenant.ShortName == name);
        }
        
        public static IEnumerable<Covenant> GetObjectsByNames(string[] names)
        {
            var covenants = Resources.LoadAll<Covenant>(Path);
            return covenants.Where(covenant => names.Contains(covenant.ShortName));
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Battle.PassiveEffects;
using Unity.VisualScripting;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu (fileName = "Item", menuName = "Config/Items")]
    public class Item : ScriptableObject
    {
        private const string Path = "Configs/Items";

        public string Name;
        public int Weight;
        public PassiveEffect ItemEffect;
        
        public static Item GetObjectByName(string name)
        {
            var items = Resources.LoadAll<Item>(Path);
            return items.FirstOrDefault(item => item.Name == name);
        }
        
        public static IEnumerable<Item> GetObjectsByNames(string[] names)
        {
            var allItems = Resources.LoadAll<Item>(Path);
            return allItems.Where(item => names.Contains(item.Name));
        }
    }
}
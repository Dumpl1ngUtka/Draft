using Battle.Abilities;
using UnityEngine;

namespace Battle.Units
{
    [CreateAssetMenu (fileName = "Class", menuName = "Config/Units/Class")]
    public class Class : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
        public int[] LineIndexes;
        public Race[] Races;
        public Ability[] Abilities;
    }
}
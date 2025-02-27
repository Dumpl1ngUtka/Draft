using UnityEngine;

namespace Battle.Units
{
    [CreateAssetMenu (fileName = "Class", menuName = "Config/Units/Class")]
    public class Class : ScriptableObject
    {
        public string Name;
        [Range(0,2)] public int LineIndex;
        public Race[] Races;
        
    }
}
using UnityEngine;

namespace Battle.Units
{
    [CreateAssetMenu(fileName = "NewRace", menuName = "Config/Units/Race")]
    public class Race : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public string[] AvailableNames;
        public Covenant[] AvailableCovenants;
        
    }
}
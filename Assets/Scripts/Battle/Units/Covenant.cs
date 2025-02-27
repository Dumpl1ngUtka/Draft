using UnityEngine;

namespace Battle.Units
{
    [CreateAssetMenu(fileName = "NEw covenant", menuName = "Config/Units/Covenant")]
    public class Covenant : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public CovenantType Type;
    }
}
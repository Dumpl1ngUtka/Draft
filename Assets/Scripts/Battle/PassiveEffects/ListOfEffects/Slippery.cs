using Units;
using UnityEngine;
 
namespace Battle.PassiveEffects
{
    [CreateAssetMenu(menuName = "Config/PassiveEffects/NoArmorEffect")]
    public class Slippery : PassiveEffect
    {
        [SerializeField] private float _baseChance = 0.7f;
        [SerializeField] private float _decreasePerDexterity = 0.05f;
        
        public float ProcChance => _baseChance + _decreasePerDexterity;
        
        protected override PassiveEffect CreateInstance(UnitStats caster, UnitStats owner)
        {
            var effect = ScriptableObject.CreateInstance<Slippery>();
            effect._baseChance = _baseChance;
            effect._decreasePerDexterity = _decreasePerDexterity;
            return effect;
        }

        protected override void AddEffect()
        {
        }

        protected override void TurnEffect()
        {
        }

        protected override void RemoveEffect()
        {
        }
    }
}
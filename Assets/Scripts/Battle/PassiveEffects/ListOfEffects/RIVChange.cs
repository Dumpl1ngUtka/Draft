using System;
using Battle.DamageSystem;
using Units;
using UnityEngine;

namespace Battle.PassiveEffects
{
    [CreateAssetMenu(menuName = "Config/PassiveEffects/RIVChange")]

    public class RIVChange : PassiveEffect
    {
        private enum Type
        {
            Attack,
            Defense,
        }
        
        [Header("RIV settings")]
        [SerializeField] private Type _type;
        [SerializeField, Range(-100, 100)] private int _value;
        [SerializeField] private DamageType _damageType;
           
        protected override PassiveEffect CreateInstance(UnitStats caster, UnitStats owner)
        {
            var effect = ScriptableObject.CreateInstance<RIVChange>();
            effect._value = _value;
            effect._type = _type;
            effect._damageType = _damageType;
            return effect;
        }

        protected override void AddEffect()
        {
            var modifier = new StatModifier(StatModifierType.AdditiveValue, value => value + _value, this);
            switch (_type)
            {
                case Type.Attack:
                    Owner.AttackRIV.GetStatBy(_damageType).AddModifier(modifier);
                    break;
                case Type.Defense:
                    Owner.DefenceRIV.GetStatBy(_damageType).AddModifier(modifier);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void TurnEffect()
        {
        }

        protected override void RemoveEffect()
        {
        }
    }
}
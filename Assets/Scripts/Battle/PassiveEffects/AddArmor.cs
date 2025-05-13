using Battle.DamageSystem;
using Battle.Units;
using UnityEngine;

namespace Battle.PassiveEffects
{    
    [CreateAssetMenu(menuName = "Config/PassiveEffects/AddArmor")]
    public class AddArmor : PassiveEffect, ICombinationEffect
    {
        [Header("Add Armor")]
        public int Value;
        
        protected override PassiveEffect CreateInstance(Unit caster, Unit owner)
        {
            var effect = ScriptableObject.CreateInstance<AddArmor>();
            effect.Value = Value;
            return effect;
        }

        protected override void AddEffect()
        {
            //Owner.Health.AddArmor(Value);
        }

        protected override void TurnEffect()
        {
        }

        protected override void RemoveEffect()
        {
            //Owner.Health.RemoveArmor(Value);
        }

        public PassiveEffect GetCombinationEffect(PassiveEffect combinationEffect)
        {
            switch (combinationEffect)
            {
                case AddArmor addArmor:
                    this.Value += addArmor.Value;
                    return this;
                case ApplyDamage applyDamage:
                    var damageValue = applyDamage.DamageType == DamageType.Acid? 
                        applyDamage.DamageValue * 2 : applyDamage.DamageValue;
                
                    if (this.Value >= damageValue)
                    {
                        this.Value -= damageValue;
                        return this; 
                    }
                    applyDamage.DamageValue -= this.Value; 
                    return applyDamage;
            }

            return null;
        }
    }
}
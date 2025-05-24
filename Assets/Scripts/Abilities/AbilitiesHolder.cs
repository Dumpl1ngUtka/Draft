using UnityEngine;

namespace Abilities
{
    public class AbilitiesHolder
    {
        public int AbilityCount;
        private readonly Ability[] _abilities;
        
        public Ability[] Abilities => _abilities;

        public AbilitiesHolder(Ability[] abilities)
        {
            AbilityCount = abilities.Length;
            _abilities = new Ability[AbilityCount];
            for (int i = 0; i < AbilityCount; i++) 
                _abilities[i] = abilities[i].GetInstance();
        }
        
        public void AddAbilityTo(int index, Ability ability)
        {
            _abilities[index] = ability;
        }

        public Ability GetAbilityByIndex(int index)
        {
            var ability = _abilities[index];
            //if (ability == null)
            //    return EmptyAbility();
            return ability;
        }
    }
}
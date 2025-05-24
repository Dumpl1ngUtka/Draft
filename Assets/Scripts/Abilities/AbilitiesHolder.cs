using System.Linq;

namespace Abilities
{
    public class AbilitiesHolder
    {
        public int AbilityCount = 6;
        private readonly Ability[] _abilities;

        public AbilitiesHolder(Ability[] abilities)
        {
            _abilities = new Ability[6];
            for (int i = 0; i < abilities.Length; i++) 
                _abilities[i] = abilities[i];
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
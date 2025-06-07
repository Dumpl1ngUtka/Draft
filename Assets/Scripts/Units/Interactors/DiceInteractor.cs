using System;
using Abilities;
using Random = UnityEngine.Random;

namespace Units.Interactors
{
    public class DiceInteractor
    {
        public int DicePower {get; private set;}
        private readonly AbilitiesHolder _abilitiesHolder;

        public Action DiceValueChanged;
        
        public DiceInteractor(AbilitiesHolder abilitiesHolder)
        {
            _abilitiesHolder = abilitiesHolder;
        }
        
        public void AddDicePower(int value)
        {
            var newPower = DicePower + value;
            if (newPower >= _abilitiesHolder.AbilityCount)
                newPower -= _abilitiesHolder.AbilityCount;
            if (newPower < 0)
                newPower += _abilitiesHolder.AbilityCount;
            DicePower = newPower;
            DiceValueChanged?.Invoke();
        }

        public void SetRandomDicePower()
        {
            DicePower = Random.Range(0,  _abilitiesHolder.AbilityCount);
            DiceValueChanged?.Invoke();
        }
    }
}
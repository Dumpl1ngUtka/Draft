using Abilities;
using Battle.PassiveEffects;
using Battle.UseCardReactions;
using Grid;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Units
{
   
    public class Unit
    {
        public GridPosition Position;
        public string Name;
        public Sprite Icon;
        public AbilitiesHolder Abilities;
        public Reaction Reaction;
        public Race Race;
        public Class Class;
        public Covenant Covenant;
        public UnitStats Stats { get; }
        public int DicePower { get; private set; }
        public bool IsReady { get; private set; } = false;
        
        public PassiveEffectsHolder PassiveEffectsHolder;
        public Ability CurrentAbility => Abilities.GetAbilityByIndex(DicePower);
        
        public Unit(UnitPreset unitPreset)
        {
            Name = unitPreset.Name;
            Icon = unitPreset.Icon;
            Reaction = unitPreset.Reaction;
            Race = unitPreset.Race;
            Class = unitPreset.Class;
            Covenant = unitPreset.Covenant;
            Stats = new UnitStats(unitPreset.Attributes);
            Abilities = new AbilitiesHolder(unitPreset.Abilities);
            PassiveEffectsHolder = new PassiveEffectsHolder();
        }

        public void EndTurn()
        {
            PassiveEffectsHolder.OnTurnEnded();
        }

        public void SetReady(bool isReady)
        {
            IsReady = isReady;
        }
        
        public void SetDicePower(int newPower)
        {
            if (newPower >= Abilities.AbilityCount)
                newPower -=  Abilities.AbilityCount;
            DicePower = newPower;
        }

        public void SetRandomDicePower()
        {
            DicePower = Random.Range(0,  Abilities.AbilityCount);
        }
    }
}
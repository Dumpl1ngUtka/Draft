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
        private UnitPreset _preset;
        
        public string Name => _preset.Name;
        public Sprite Icon => _preset.Icon;
        public AbilitiesHolder Abilities;
        public Reaction Reaction => _preset.Reaction;
        public Race Race => _preset.Race;
        public Class Class => _preset.Class;
        public Covenant Covenant => _preset.Covenant;
        public UnitStats Stats { get; }
        public TeamType TeamType { get; private set; }
        public int DicePower { get; private set; }
        public bool IsReady { get; private set; } = false;
        
        public PassiveEffectsHolder PassiveEffectsHolder;
        public Ability CurrentAbility => Abilities.GetAbilityByIndex(DicePower);
        
        public Unit(UnitPreset unitPreset)
        {
            _preset = unitPreset; 
            Stats = new UnitStats(unitPreset.Attributes);
            Abilities = new AbilitiesHolder(_preset.Abilities);
            PassiveEffectsHolder = new PassiveEffectsHolder();
        }

        public void EndTurn()
        {
            PassiveEffectsHolder.OnTurnEnded();
        }

        public void SetTeam(TeamType teamIndex)
        {
            TeamType = teamIndex;
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
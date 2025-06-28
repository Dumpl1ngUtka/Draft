using System;
using System.Linq;
using Abilities;
using Battle.PassiveEffects;
using Battle.UseCardReactions;
using Grid;
using Items;
using Units.Interactors;
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
        public DiceInteractor DiceInteractor { get; }
        public PassiveEffectsHolder PassiveEffectsHolder { get; }
        public ItemsHolder ItemsHolder { get; }
        public Ability CurrentAbility => Abilities.GetAbilityByIndex(DiceInteractor.DicePower);
        
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
            DiceInteractor = new DiceInteractor(Abilities);
            PassiveEffectsHolder = Race != null ? 
                new PassiveEffectsHolder(Race.GetPassiveEffects(this)) : new PassiveEffectsHolder();
            ItemsHolder = new ItemsHolder(Stats, PassiveEffectsHolder);
        }

        public Unit WithHealth(int health)
        {
            var startHPModifier = new StatModifier(StatModifierType.BeforeBaseValueAddition, value => health);
            Stats.CurrentHealth.AddModifier(startHPModifier);
            return this;
        }
        
        public Unit WithPosition(GridPosition position)
        {
            Position = position;
            return this;    
        }

        public Unit WithItems(Item[] items)
        {
            ItemsHolder.AddItems(items);
            return this;
        }

        public void EndTurn()
        {
            PassiveEffectsHolder.OnTurnEnded();
        }
    }
}
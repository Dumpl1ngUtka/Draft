using System;
using System.Collections.Generic;
using Battle.Abilities;
using Battle.DamageSystem;
using Battle.PassiveEffects;
using Battle.UseCardReactions;
using Grid;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

namespace Battle.Units
{
   
    public class Unit 
    {
        private const int _maxChem = 10;
        private int _chemistry = 0;
        private UnitPreset _preset;
        
        public string Name => _preset.Name;
        public Sprite Icon => _preset.Icon;
        public Attributes Attributes => _preset.Attributes;
        public List<DamageType> Immunities => _preset.Immunities;
        public List<DamageType> Resistances => _preset.Resistances;
        public List<DamageType> Vulnerability => _preset.Vulnerabilities;
        public Ability[] Abilities => _preset.Abilities;
        public Reaction Reaction => _preset.Reaction;
        public Race Race => _preset.Race;
        public Class Class => _preset.Class;
        public Covenant Covenant => _preset.Covenant;
        public int Chemistry => _chemistry;
        public UnitStats Stats { get; }
        public TeamType TeamType { get; private set; }
        public UnitHealth Health { get; private set; }
        public int DicePower { get; private set; }
        public bool IsDead { get; private set; } = false;
        public bool IsReady { get; private set; } = false;
        
        public PassiveEffectsHolder PassiveEffectsHolder;
        public Ability CurrentAbility => Abilities[DicePower];
        
        public Action ParametersChanged;
        public Action HealthChanged;
        public Action ChemistryChanged;
        public Action ReadyStatusChanged;
        public Action DicePowerChanged;
        public Action TurnEnded;
        public Action PassiveEffectsChanged;
        
        public Unit(UnitPreset unitPreset)
        {
            _preset = unitPreset; 
            Stats = new UnitStats(Attributes);
            InitHealth();
            ParametersChanged?.Invoke();
            PassiveEffectsHolder = new PassiveEffectsHolder(this);
            PassiveEffectsHolder.PassiveEffectsChanged += () => PassiveEffectsChanged?.Invoke();
        }
        
        private void InitHealth()
        {
            Health = new UnitHealth(this);
            Health.OnDead += OnDead;
            Health.OnValueChanged += () => HealthChanged?.Invoke();
        }

        private void OnDead()
        {
            IsDead = true;
        }

        public void SetChemistry(int chemestry)
        {
            _chemistry = chemestry > _maxChem ? _maxChem : chemestry;
            ChemistryChanged?.Invoke();
        }

        public void EndTurn()
        {
            TurnEnded?.Invoke();
        }

        public void SetTeam(TeamType teamIndex)
        {
            TeamType = teamIndex;
        }

        public void SetReady(bool isReady)
        {
            IsReady = isReady;
            ReadyStatusChanged?.Invoke();
        }
        
        public void SetDicePower(int newPower)
        {
            if (newPower >= Abilities.Length)
                newPower -=  Abilities.Length;
            DicePower = newPower;
            DicePowerChanged?.Invoke();
        }

        public void SetRandomDicePower()
        {
            DicePower = Random.Range(0,  Abilities.Length);
            DicePowerChanged?.Invoke();
        }
    }
}
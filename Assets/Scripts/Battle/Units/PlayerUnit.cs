using System;
using System.Collections.Generic;
using Battle.Abilities;
using Battle.DamageSystem;
using Battle.UseCardReactions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battle.Units
{
    public class PlayerUnit : Unit  
    {
        private const int _maxChem = 10;
        private const int _startAttributePoints = 40;

        private string _name;
        private Attributes _attributes;
        private Reaction _reaction;
        
        public int Chemistry { get; protected  set; }
        public Race Race { get; protected  set; }
        public Class Class { get; protected  set; } 
        public Covenant Covenant { get; protected  set; }
        public override string Name => _name;
        public override Sprite Icon => Class.Icon;
        public override Attributes Attributes => _attributes;
        public override List<DamageType> Immunities => Race.Immunities;
        public override List<DamageType> Resistances => Race.Resistances;
        public override List<DamageType> Vulnerability => Race.Vulnerability;
        public override Ability[] Abilities => Class.Abilities;
        public override Reaction Reaction => _reaction;

        public bool IsMaxChem => Chemistry == _maxChem;
        
        public void Init(Class unitClass)
        {
            Class = unitClass; 
            Race = Class.Races[Random.Range(0, Class.Races.Length)];
            _reaction = Race.Reaction;
            _name = Race.AvailableNames[Random.Range(0, Race.AvailableNames.Length)];
            Covenant = Race.AvailableCovenants[Random.Range(0, Race.AvailableCovenants.Length)];
            _attributes = new Attributes(_startAttributePoints);
        }
        
        public void SetChemistry(int chemestry)
        {
            Chemistry = chemestry >_maxChem ? _maxChem : chemestry;
            ParametersChanged?.Invoke();
        }

    }
}
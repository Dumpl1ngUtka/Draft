using System;
using System.Collections.Generic;
using Battle.Abilities;
using Battle.DamageSystem;
using Battle.UseCardReactions;
using Unity.VisualScripting;
using UnityEngine;

namespace Battle.Units
{
    [CreateAssetMenu(menuName = "Config/Enemy")]
    public class Enemy : Unit
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Attributes _attributes;
        [SerializeField] private List<DamageType> _immunities;
        [SerializeField] private List<DamageType> _resistances;
        [SerializeField] private List<DamageType> _vulnerabilities;
        [SerializeField] private Ability[] _abilities;
        [SerializeField] private Reaction _reaction;
        public override string Name => _name;
        public override Sprite Icon => _icon;
        public override Attributes Attributes => _attributes;
        public override List<DamageType> Immunities => _immunities;
        public override List<DamageType> Resistances => _resistances;
        public override List<DamageType> Vulnerability => _vulnerabilities;
        public override Ability[] Abilities => _abilities;
        public override Reaction Reaction => _reaction;
    }
}
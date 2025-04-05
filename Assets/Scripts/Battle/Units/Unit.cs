using System;
using System.Collections.Generic;
using Battle.Abilities;
using Battle.DamageSystem;
using Battle.UseCardReactions;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

namespace Battle.Units
{
    public abstract class Unit : ScriptableObject
    {
        public abstract string Name { get; }
        public abstract Sprite Icon { get; }
        public abstract Attributes Attributes  { get; }
        public abstract List<DamageType> Immunities  { get; }
        public abstract List<DamageType> Resistances  { get; }
        public abstract List<DamageType> Vulnerability { get; }
        public abstract Ability[] Abilities { get; }
        public abstract Reaction Reaction { get; }
        public Action ParametersChanged;
    }
}
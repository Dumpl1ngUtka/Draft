using System;
using System.Collections.Generic;
using Battle.DamageSystem;
using UnityEngine;

namespace Battle.Units
{
    public class UnitStats
    {
        #region Attributes

        private readonly Attributes _attributes;
        public StatInt HealthAttribute;
        public StatInt StrengthAttribute;
        public StatInt DexterityAttribute;
        public StatInt IntelligenceAttribute;
        
        public StatInt GetAttributeByType(AttributesType type)
        {
            return type switch
            {
                AttributesType.Health => HealthAttribute,
                AttributesType.Intelligence => IntelligenceAttribute,
                AttributesType.Strength => StrengthAttribute,
                AttributesType.Dexterity => DexterityAttribute,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public Action AttributeChanged;
        
        #endregion
        
        #region Immunities
        
        public List<DamageType> Immunities => new List<DamageType>();
        public List<DamageType> Resistances =>  new List<DamageType>();
        public List<DamageType> Vulnerability => new List<DamageType>();
        
        #endregion
        
        #region Health
        
        public StatInt MaxHealth;
        public StatInt CurrentHealth;
        public StatInt Armor;
        public bool IsDead => CurrentHealth.Value <= 0;

        public Action HealthChanged;
        
        #endregion

        public UnitStats(Unit unit)
        {
            _attributes = unit.Attributes;
            InitializeStats();
            InitializedActions();
        }

        private void InitializeStats()
        {
            HealthAttribute = new StatInt(_attributes.Health);
            StrengthAttribute = new StatInt(_attributes.Strength);
            DexterityAttribute = new StatInt(_attributes.Dexterity);
            IntelligenceAttribute = new StatInt(_attributes.Intelligence);
            
            MaxHealth = new StatInt(20);
            MaxHealth.AddModifier(new StatModifier(StatModifierType.BeforeBaseValueAddition, 
                value => value + HealthAttribute.Value * 3));
            
            Armor = new StatInt(0, true);
            //Armor.AddModifier(new StatModifier(StatModifierType.SystemModifier, 
            //    value => Math.Max(value, 0)));
            
            CurrentHealth = new StatInt(MaxHealth.Value);
            CurrentHealth.AddModifier(new StatModifier(StatModifierType.SystemModifier, 
                value => Math.Clamp(value, 0, MaxHealth.Value)));
            Debug.Log(CurrentHealth.Value + " / " + MaxHealth.Value);
        }

        private void InitializedActions()
        {
            HealthAttribute.StatChanged += () => AttributeChanged?.Invoke();
            StrengthAttribute.StatChanged += () => AttributeChanged?.Invoke();
            DexterityAttribute.StatChanged += () => AttributeChanged?.Invoke();
            IntelligenceAttribute.StatChanged += () => AttributeChanged?.Invoke();
            
            MaxHealth.StatChanged += () => HealthChanged?.Invoke();
            CurrentHealth.StatChanged += () => HealthChanged?.Invoke();
            Armor.StatChanged += () => HealthChanged?.Invoke();

            AttributeChanged += SetCurrentValueOutdated;
            HealthChanged += SetCurrentValueOutdated;
        }

        private void SetCurrentValueOutdated()
        {
            HealthAttribute.CurrentValueOutdated();
            StrengthAttribute.CurrentValueOutdated();
            DexterityAttribute.CurrentValueOutdated();
            IntelligenceAttribute.CurrentValueOutdated();
            
            MaxHealth.CurrentValueOutdated();
            CurrentHealth.CurrentValueOutdated();
            Armor.CurrentValueOutdated();
        }
    }
}
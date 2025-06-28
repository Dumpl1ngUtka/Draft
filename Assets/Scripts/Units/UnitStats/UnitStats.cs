using System;
using System.Collections.Generic;
using Battle.DamageSystem;

namespace Units
{
    public class UnitStats
    {
        #region Attributes

        public readonly Attributes Attributes;
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

        #region Other

        public StatInt Energy;
        public StatInt Chemistry;
        public StatInt Capacity;

        public bool IsReady => Energy.Value > 0 && !IsDead;
        
        #endregion
        
        
        public UnitStats(Attributes attributes)
        {
            Attributes = attributes;
            InitializeStats();
            InitializedActions();
        }

        private void InitializeStats()
        {
            HealthAttribute = new StatInt(Attributes.Health);
            StrengthAttribute = new StatInt(Attributes.Strength);
            DexterityAttribute = new StatInt(Attributes.Dexterity);
            IntelligenceAttribute = new StatInt(Attributes.Intelligence);
            
            MaxHealth = new StatInt(20);
            MaxHealth.AddModifier(new StatModifier(StatModifierType.BeforeBaseValueAddition, 
                value => value + HealthAttribute.Value * 3));
            
            CurrentHealth = new StatInt(MaxHealth.Value);
            CurrentHealth.AddModifier(new StatModifier(StatModifierType.SystemModifier, 
                value => Math.Clamp(value, 0, MaxHealth.Value)));
            
            Armor = new StatInt(0, true);
            //Armor.AddModifier(new StatModifier(StatModifierType.SystemModifier, 
            //    value => Math.Max(value, 0)));
            
            Chemistry = new StatInt(0);
            Chemistry.AddModifier(new StatModifier(StatModifierType.SystemModifier,
                value => Math.Min(10, value)));
            
            Energy = new StatInt(1);
            Capacity = new StatInt(5);
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
using System;
using System.Collections.Generic;
using Battle.DamageSystem;
using UnityEngine;
using IObserver = CustomObserver.IObserver<Units.UnitStats>;

namespace Units
{
    public class UnitStats : CustomObserver.IObservable<UnitStats>
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

        #region Other

        public StatInt Energy;
        public StatInt Chemistry;

        public bool IsReady => Energy.Value > 0;
        
        #endregion
        
        
        public UnitStats(Attributes attributes)
        {
            _attributes = attributes;
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
            
            Chemistry.StatChanged += NotifyObservers;

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
            
            NotifyObservers();
        }

        #region Observers

        private readonly List<IObserver> _observers = new();
        
        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers) observer.UpdateObserver(this);
        }
        
        #endregion
        
    }
}
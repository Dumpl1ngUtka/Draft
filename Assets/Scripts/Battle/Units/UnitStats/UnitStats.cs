using System;
using System.Collections.Generic;
using Battle.PassiveEffects;

namespace Battle.Units
{
    public class UnitStats
    {
        private int _currentHealth;

        #region Attributes

        private readonly Attributes _attributes;
        public StatInt HealthAttribute;
        public StatInt StrengthAttribute;
        public StatInt DexterityAttribute;
        public StatInt IntelligenceAttribute;
        
        #endregion
        
        public StatInt MaxHealth;

        public int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                _currentHealth = Math.Clamp(value, 0, MaxHealth.Value);
                if (_currentHealth == 0)
                    ;//call action 
            }
        }

        public StatInt Armor;


        public UnitStats(Attributes unitAttributes)
        {
            _attributes = unitAttributes;
            InitializeStats();
        }

        private void InitializeStats()
        {
            HealthAttribute = new StatInt(_attributes.Health);
            StrengthAttribute = new StatInt(_attributes.Strength);
            DexterityAttribute = new StatInt(_attributes.Dexterity);
            IntelligenceAttribute = new StatInt(_attributes.Intelligence);
            
            MaxHealth = new StatInt(20);
            MaxHealth.AddFunc(HealthAttributesDependency);
            
            Armor = new StatInt(0);
            CurrentHealth = MaxHealth.Value;
        }

        private float HealthAttributesDependency(float rawValue)
        {
            return rawValue + HealthAttribute.Value * 3;
        }
    }
}
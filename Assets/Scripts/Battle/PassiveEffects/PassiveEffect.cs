using System;
using Battle.DamageSystem;
using Battle.Units;
using Units;
using UnityEngine;

namespace Battle.PassiveEffects
{
    public abstract class PassiveEffect : ScriptableObject
    {
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected Color _color;
        [SerializeField] protected int _turnCount;
        [Header("Animation Clips")]
        [SerializeField] protected AnimationClip _addClip;
        [SerializeField] protected AnimationClip _turnClip;
        [SerializeField] protected AnimationClip _destroyClip;
        protected UnitStats Caster;
        protected UnitStats Owner;
        
        public int TurnCount => _turnCount;
        public Sprite Icon => _icon;
        public Color Color => _color;
        
        public void ReduceCount() => _turnCount--;
        
        public void OnAdd()
        {
            AddEffect();
        }

        public void OnTurnEnded()
        {
            TurnEffect();
        }

        public void Destroy()
        {
            RemoveEffect();
            Destroy(this);
        }

        public AnimationClip GetClipByType(TriggerType type)
        {
            return type switch
            {
                TriggerType.Add => _addClip,
                TriggerType.TurnEnded => _turnClip,
                TriggerType.Destroy => _destroyClip,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public PassiveEffect GetInstance(UnitStats caster, UnitStats owner)
        {
            var instance = CreateInstance(caster, owner);
            instance._turnCount = _turnCount;
            instance._icon = _icon;
            instance._color = _color;
            instance.Caster = caster;
            instance.Owner = owner;
            instance._addClip = _addClip;
            instance._turnClip = _turnClip;
            instance._destroyClip = _destroyClip;
            return instance;
        }
        protected abstract PassiveEffect CreateInstance(UnitStats caster, UnitStats owner);
        
        protected abstract void AddEffect();
        
        protected abstract void TurnEffect();
        
        protected abstract void RemoveEffect();
    }

    public enum TriggerType
    {
        Add,
        TurnEnded,
        Destroy,
    }
}
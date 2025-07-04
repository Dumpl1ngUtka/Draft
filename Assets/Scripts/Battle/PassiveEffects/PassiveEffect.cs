using System;
using Units;
using UnityEngine;

namespace Battle.PassiveEffects
{
    public abstract class PassiveEffect : ScriptableObject
    {
        [SerializeField] private string _dbKey;
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected Color _color = Color.white;
        [Header("Animation Clips")]
        [SerializeField] protected AnimationClip _addClip;
        [SerializeField] protected AnimationClip _turnClip;
        [SerializeField] protected AnimationClip _destroyClip;
        [SerializeField] protected AnimationClip _effectClip; //if add some new field, update GetInstance method
        protected UnitStats Caster;
        protected UnitStats Owner;
        private int _turnCount;
        
        public string DBKey => _dbKey;
        public int TurnCount => _turnCount;
        public Sprite Icon => _icon;
        public Color Color => _color;
        
        public void ReduceCount() => _turnCount--;
        
        public Action EffectRemoved;
        
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
            EffectRemoved?.Invoke();
            Destroy(this);
        }

        public AnimationClip GetClipByType(TriggerType type)
        {
            return type switch
            {
                TriggerType.Add => _addClip,
                TriggerType.TurnEnded => _turnClip,
                TriggerType.Destroy => _destroyClip,
                TriggerType.Effect => _effectClip,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public PassiveEffect GetInstance(UnitStats caster, UnitStats owner, int turnCount)
        {
            var instance = CreateInstance(caster, owner);
            instance._dbKey = _dbKey;
            instance._turnCount = turnCount;
            instance._icon = _icon;
            instance._color = _color;
            instance.Caster = caster;
            instance.Owner = owner;
            instance._addClip = _addClip;
            instance._turnClip = _turnClip;
            instance._destroyClip = _destroyClip;
            instance._effectClip = _effectClip;
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
        Effect,
    }
}
using Battle.DamageSystem;
using Battle.Units;
using UnityEngine;

namespace Battle.PassiveEffects
{
    public abstract class PassiveEffect : ScriptableObject
    {
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected Color _color;
        [SerializeField] protected int _turnCount;
        protected Unit Caster;
        protected Unit Owner;
        
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
        
        public abstract PassiveEffect GetInstance(Unit caster, Unit owner);
        
        protected abstract void AddEffect();
        
        protected abstract void TurnEffect();
        
        protected abstract void RemoveEffect();
    }
}
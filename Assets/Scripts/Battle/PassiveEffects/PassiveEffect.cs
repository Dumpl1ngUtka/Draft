using Battle.DamageSystem;
using Battle.Units;
using UnityEngine;

namespace Battle.PassiveEffects
{
    public abstract class PassiveEffect : ScriptableObject
    {
        [SerializeField] private int _turnCount;
        protected Unit Caster;
        protected Unit Owner;
        
        public int TurnCount => _turnCount;
        
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
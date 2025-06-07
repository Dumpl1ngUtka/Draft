using System.Collections.Generic;
using Grid.Cells;
using Units;
using UnityEngine;

namespace Battle.UseCardReactions
{
    public abstract class Reaction : ScriptableObject
    {
        [SerializeField] private int _value = 1;
        
        public abstract List<Unit> GetReactionCells(Unit caster, List<Unit> allies);

        public void UseReaction(Unit caster, List<Unit> allies)
        {
            foreach (var unit in GetReactionCells(caster, allies)) 
                unit.DiceInteractor.AddDicePower(_value);
        }
    }
}

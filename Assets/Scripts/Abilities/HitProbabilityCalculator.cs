using System;
using Grid.Cells;
using Units;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public class HitProbabilityCalculator
    {
        [SerializeField, Range(0f, 1f)] private float _baseHitProbability;
        [Header("Caster")]
        [SerializeField] private AttributesType _casterAttributType;
        [SerializeField, Range(0f, 1f)] private float _additionProbabilityByAttribute;
        [Header("Target")]
        [SerializeField] private AttributesType _targetAttributeType;
        [SerializeField, Range(0f, 1f)] private float _subtractionProbabilityByAttribute;
        [Header("Distance")]
        [SerializeField, Range(0f, 1f)] private float _additionProbabilityByDistance;
        
        public float GetHitProbability(Unit caster, Unit target)
        {
            var probability = _baseHitProbability;
            probability += caster.Stats.GetAttributeByType(_casterAttributType).Value 
                           * _additionProbabilityByAttribute;            
            probability -= target.Stats.GetAttributeByType(_targetAttributeType).Value 
                           * _subtractionProbabilityByAttribute;
            probability += CalculateDistance(caster.Position, target.Position) * _additionProbabilityByDistance;
            return Mathf.Clamp01(probability);
        } 
        
        private int CalculateDistance(GridPosition cell1, GridPosition cell2)
        {
            if (cell1.TeamType == cell2.TeamType)
                return Math.Abs(cell1.LineIndex - cell2.LineIndex);
            
            return cell1.LineIndex + cell2.LineIndex + 1;
        }
    }
}
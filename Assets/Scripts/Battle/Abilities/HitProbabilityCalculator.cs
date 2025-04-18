using System;
using Battle.Grid;
using Battle.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Abilities
{
    [Serializable]
    public class HitProbabilityCalculator
    {
        [SerializeField, Range(0f, 1f)] private float _baseHitProbability;
        [Header("Caster")]
        [SerializeField] private AttributesType _casterAttributType;
        [SerializeField] private float _additionProbabilityByAttribute;
        [Header("Target")]
        [SerializeField] private AttributesType _targetAttributeType;
        [SerializeField] private float _subtractionProbabilityByAttribute;
        [Header("Distance")]
        [SerializeField] private float _additionProbabilityByDistance;

        public float GetHitProbability(GridCell caster, GridCell target)
        {
            var probability = _baseHitProbability;
            probability += caster.Unit.Attributes.GetAttributeValueByType(_casterAttributType) 
                           * _additionProbabilityByAttribute;            
            probability -= target.Unit.Attributes.GetAttributeValueByType(_targetAttributeType) 
                           * _subtractionProbabilityByAttribute;
            probability += CalculateDistance(caster, target) * _additionProbabilityByDistance;
            return Mathf.Clamp01(probability);
        } 
        
        private int CalculateDistance(GridCell cell1, GridCell cell2)
        {
            if (cell1.TeamIndex == cell2.TeamIndex)
                return Math.Abs(cell1.LineIndex - cell2.LineIndex);
            
            return cell1.LineIndex + cell2.LineIndex + 1;
        }
    }
}
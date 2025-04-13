using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.PassiveEffects;
using Battle.Units;
using Battle.Units.Interactors;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Abilities
{
    [CreateAssetMenu(menuName = "Config/Ability")]
    public class Ability : ScriptableObject 
    {
        public string Name;
        public Sprite Icon;
        public PassiveEffect AbillityEffect;
        public AbilityTargetFilter _targetFilter;
        public AbilityRangeFilter _rangeFilter;
        public HitProbabilityCalculator HitProbabilityCalculator;

        public float GetHitProbability(GridCell caster, GridCell target)
        {
            if (!IsRightTarget(caster, target))
                return 0;
            return HitProbabilityCalculator.GetHitProbability(caster, target);
        }

        public List<GridCell> GetRange(GridCell caster, GridCell target, List<GridCell> allies, List<GridCell> enemies)
        {
            var rangeCells = _rangeFilter.GetRelevantCells(caster, target, allies, enemies);
            return rangeCells.Where(x =>  _targetFilter.IsRightTarget(caster, x)).ToList();
        }
        
        public bool IsRightTarget(GridCell caster, GridCell target) => _targetFilter.IsRightTarget(caster, target);

        public Response TryUseAbility(GridCell caster, GridCell target, List<GridCell> allies, List<GridCell> enemies)
        {
            if (!_targetFilter.IsRightTarget(caster, target))
                return new Response(false, "wrong_target_error");
            
            var rangeCells = _rangeFilter.GetRelevantCells(caster, target, allies, enemies);
            foreach (var cell in rangeCells)
            {
                var targetUnit = cell.Unit;
                var effect = AbillityEffect.GetInstance(caster.Unit, targetUnit);
                targetUnit.PassiveEffectsHolder.AddEffect(effect);
            }
            return new Response(true, "success");
        }

        public GridCell GetPreferredTarget(List<GridCell> potentialTargets)
        {
            return potentialTargets[Random.Range(0, potentialTargets.Count)];       
        }
    }
}
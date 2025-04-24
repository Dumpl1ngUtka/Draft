using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.PassiveEffects;
using Battle.Units;
using Battle.Units.Interactors;
using Grid.Cells;
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

        public float GetHitProbability(UnitGridCell caster, UnitGridCell target)
        {
            if (!IsRightTarget(caster, target))
                return 0;
            return HitProbabilityCalculator.GetHitProbability(caster, target);
        }

        public List<UnitGridCell> GetRange(UnitGridCell caster, UnitGridCell target, List<UnitGridCell> allies, List<UnitGridCell> enemies)
        {
            var rangeCells = _rangeFilter.GetRelevantCells(caster, target, allies, enemies);
            return rangeCells.Where(x =>  _targetFilter.IsRightTarget(caster, x)).ToList();
        }
        
        public bool IsRightTarget(UnitGridCell caster, UnitGridCell target) => _targetFilter.IsRightTarget(caster, target);

        public Response TryUseAbility(UnitGridCell caster, UnitGridCell target, List<UnitGridCell> allies, List<UnitGridCell> enemies)
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

        public UnitGridCell GetPreferredTarget(List<UnitGridCell> potentialTargets)
        {
            return potentialTargets[Random.Range(0, potentialTargets.Count)];       
        }
    }
}
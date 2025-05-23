using System.Collections.Generic;
using System.Linq;
using Battle.Abilities;
using Battle.PassiveEffects;
using Battle.Units.Interactors;
using Grid.Cells;
using UnityEngine;
using UnityEngine.Serialization;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Config/Ability")]
    public class Ability : ScriptableObject 
    {
        public string Name;
        public Sprite Icon;
        public PassiveEffect AbillityEffect;
        public AbilityTargetFilter TargetFilter;
        public AbilityRangeFilter RangeFilter;
        public HitProbabilityCalculator HitProbabilityCalculator;

        public Ability GetInstance()
        {
            var instance = ScriptableObject.CreateInstance<Ability>();
            instance.Name = this.Name;
            instance.Icon = this.Icon;
            instance.AbillityEffect = this.AbillityEffect;
            instance.TargetFilter = this.TargetFilter;
            instance.RangeFilter = this.RangeFilter;
            instance.HitProbabilityCalculator = this.HitProbabilityCalculator;
            return instance;
        }
        
        public float GetHitProbability(UnitGridCell caster, UnitGridCell target)
        {
            if (!IsRightTarget(caster, target))
                return 0;
            return HitProbabilityCalculator.GetHitProbability(caster, target);
        }

        public List<UnitGridCell> GetRange(UnitGridCell caster, UnitGridCell target, List<UnitGridCell> allies, List<UnitGridCell> enemies)
        {
            var rangeCells = RangeFilter.GetRelevantCells(caster, target, allies, enemies);
            return rangeCells.Where(x =>  TargetFilter.IsRightTarget(caster, x)).ToList();
        }
        
        public bool IsRightTarget(UnitGridCell caster, UnitGridCell target) => TargetFilter.IsRightTarget(caster, target);

        public Response TryUseAbility(UnitGridCell caster, UnitGridCell target, List<UnitGridCell> allies, List<UnitGridCell> enemies)
        {
            if (!caster.Unit.IsReady)
            {
                return new Response(false, "is_not_ready_error");
            }
            
            if (!TargetFilter.IsRightTarget(caster, target))
                return new Response(false, "wrong_target_error");
            
            var rangeCells = RangeFilter.GetRelevantCells(caster, target, allies, enemies);
            foreach (var cell in rangeCells)
            {
                var targetUnit = cell.Unit;
                var effect = AbillityEffect.GetInstance(caster.Unit.Stats, targetUnit.Stats);
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
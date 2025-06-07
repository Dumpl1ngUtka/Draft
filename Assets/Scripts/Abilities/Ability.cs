using System.Collections.Generic;
using System.Linq;
using Battle.PassiveEffects;
using Units;
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
        public EnergyReduce EnergyReduceEffect;
        
        public Ability GetInstance()
        {
            var instance = ScriptableObject.CreateInstance<Ability>();
            instance.Name = this.Name;
            instance.Icon = this.Icon;
            instance.AbillityEffect = this.AbillityEffect;
            instance.TargetFilter = this.TargetFilter;
            instance.RangeFilter = this.RangeFilter;
            instance.HitProbabilityCalculator = this.HitProbabilityCalculator;
            instance.EnergyReduceEffect = this.EnergyReduceEffect;
            return instance;
        }
        
        public float GetHitProbability(Unit caster, Unit target)
        {
            return HitProbabilityCalculator.GetHitProbability(caster, target);
        }

        public List<Unit> GetRange(Unit caster, Unit target, List<Unit> allies, List<Unit> enemies)
        {
            var rangeCells = RangeFilter.GetRelevantCells(target, allies, enemies);
            return rangeCells.Where(x =>  TargetFilter.IsRightTarget(caster, x)).ToList();
        }
        
        public bool IsRightTarget(Unit caster, Unit target)
        {
            return TargetFilter.IsRightTarget(caster, target);
        }

        public void UseAbility(Unit caster, Unit target, List<Unit> allies, List<Unit> enemies)
        {
            var rangeCells = RangeFilter.GetRelevantCells(target, allies, enemies);
            foreach (var cell in rangeCells)
            {
                var effect = AbillityEffect.GetInstance(caster.Stats, cell.Stats);
                cell.PassiveEffectsHolder.AddEffect(effect);
            }
            caster.PassiveEffectsHolder.AddEffect(EnergyReduceEffect.GetInstance(caster.Stats, caster.Stats));
        }

        public Unit GetPreferredTarget(List<Unit> potentialTargets)
        {
            return potentialTargets[Random.Range(0, potentialTargets.Count)];       
        }
    }
}
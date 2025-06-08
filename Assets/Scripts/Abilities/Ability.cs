using System.Collections.Generic;
using System.Linq;
using Battle.PassiveEffects;
using Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Config/Ability")]
    public class Ability : ScriptableObject 
    {
        public string Name;
        public Sprite Icon;
        public AbilityTargetFilter TargetFilter;
        public AbilityRangeFilter RangeFilter;
        public List<AbilityEffectHolder> Effects;
        public EnergyReduce EnergyReduceEffect;
        
        public Ability GetInstance()
        {
            var instance = ScriptableObject.CreateInstance<Ability>();
            instance.Name = this.Name;
            instance.Icon = this.Icon;
            instance.Effects = this.Effects;
            instance.TargetFilter = this.TargetFilter;
            instance.RangeFilter = this.RangeFilter;
            instance.EnergyReduceEffect = this.EnergyReduceEffect;
            return instance;
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
                foreach (var holder in Effects)
                {
                    if (IsEffectProc(holder.Probability, caster, target))
                    {
                        var effect = holder.Effect.GetInstance(caster.Stats, cell.Stats);
                        cell.PassiveEffectsHolder.AddEffect(effect);
                    }
                }
            }
            caster.PassiveEffectsHolder.AddEffect(EnergyReduceEffect.GetInstance(caster.Stats, caster.Stats));
        }

        public Unit GetPreferredTarget(Unit caster, List<Unit> allies, List<Unit> enemies)
        {
            var potentialTargets = allies.Concat(enemies).Where(cell => IsRightTarget(caster, cell)).ToList();
            return !potentialTargets.Any() ? null : potentialTargets[Random.Range(0, potentialTargets.Count)];
        }

        private bool IsEffectProc(HitProbabilityCalculator effectProbability, Unit caster, Unit target)
        {
            var random = Random.Range(0f, 1f);
            return random < effectProbability.GetHitProbability(caster, target);  
        }
    }
}
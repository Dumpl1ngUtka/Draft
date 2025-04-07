using System.Collections.Generic;

namespace Battle.Units.Interactors.Ability
{
    public class UseAbilityInteractor
    {
        public Unit GetPreferredTarget(int power, Unit caster, List<Unit> potentialTargets)
        {
            var ability = caster.GetAbilityByPower(power);
            return ability.GetPreferredTarget(potentialTargets);
        }
        
        public Response UseAbility(int power, Unit caster, Unit target, List<Unit> allies, List<Unit> enemies)
        {
            if (!caster.IsReady)
                return new Response(false, "unit_not_ready_error");
            
            var ability = caster.GetAbilityByPower(power);
            if (ability == null)
                return new Response(false, "ability_not_found_error");
            
            var response = ability.TryUseAbility(caster, target, allies, enemies);
                if (response.Success)
                    caster.SetReady(false);
            return response;
        }
    }
}
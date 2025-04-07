using System.Collections.Generic;
using Battle.Grid;

namespace Battle.Units.Interactors.Reaction
{
    public class UseReactionInteractor
    {
        public Response UseReaction(GridCell caster, List<GridCell> allies)
        {
            var reaction = caster.Unit.Reaction;
            var response = reaction.TryUseReaction(caster, allies);
            return response;
        }
    }
}
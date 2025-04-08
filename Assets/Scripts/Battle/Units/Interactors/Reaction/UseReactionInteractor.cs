using System.Collections.Generic;
using System.Linq;
using Battle.Grid;

namespace Battle.Units.Interactors.Reaction
{
    public class UseReactionInteractor
    {
        public List<GridCell> GetReactionCells(GridCell caster, List<GridCell> allies)
        {
            var reaction = caster.Unit.Reaction;
            var cells = reaction.GetReactionCells(caster, allies);
            return cells.Where(cell => cell.Unit.IsReady).ToList();
        }
        
        public Response UseReaction(GridCell caster, List<GridCell> allies)
        {
            var reaction = caster.Unit.Reaction;
            var response = reaction.TryUseReaction(caster, allies);
            return response;
        }
    }
}
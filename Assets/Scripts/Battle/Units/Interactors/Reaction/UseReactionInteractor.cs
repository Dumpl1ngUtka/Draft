using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Grid.Cells;

namespace Battle.Units.Interactors.Reaction
{
    public class UseReactionInteractor
    {
        public List<UnitGridCell> GetReactionCells(UnitGridCell caster, List<UnitGridCell> allies)
        {
            var reaction = caster.Unit.Reaction;
            var cells = reaction.GetReactionCells(caster, allies);
            return cells.Where(cell => cell.Unit.IsReady).ToList();
        }
        
        public Response UseReaction(UnitGridCell caster, List<UnitGridCell> allies)
        {
            var reaction = caster.Unit.Reaction;
            var response = reaction.TryUseReaction(caster, allies);
            return response;
        }
    }
}
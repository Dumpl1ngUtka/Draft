using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.Units;
using Battle.Units.Interactors.Reaction;
using Grid.Cells;
using Services.GameControlService;
using Services.PanelService;

namespace Grid.BattleGrid
{
    public class BattleGridModel : GridModel
    {
        private List<Unit> _playerUnits;
        private List<Unit> _enemyUnits;        
        private List<UnitGridCell> _playerCells;
        private List<UnitGridCell> _enemyCells;
        private readonly UseReactionInteractor _useReactionInteractor;
        private readonly TurnInteractor _turnInteractor;

        public BattleGridModel(List<UnitGridCell> playerCells, List<UnitGridCell> enemyCells)
        {
            _useReactionInteractor = new UseReactionInteractor();
            _playerCells = playerCells;
            _enemyCells = enemyCells;
            _turnInteractor = new TurnInteractor(_playerCells, _enemyCells);
            _playerUnits = playerCells.Select(x => x.Unit).ToList();
            _enemyUnits = enemyCells.Select(x => x.Unit).ToList();
        }
        
        public void EndTurn()
        {
            _turnInteractor.EndTurn();
        }
        
        public void UseAbility(UnitGridCell from, UnitGridCell to)
        {
            var response = from.Unit.CurrentAbility.TryUseAbility(from, to, _playerCells, _enemyCells);
            if (response.Success)
            {
                from.Unit.SetReady(false);
            }
            else
            {
                PanelService.Instance.InstantiateErrorPanel(response.Message);
                return;
            }
            
            response = _useReactionInteractor.UseReaction(from, _playerCells);
            if (!response.Success)
            {
                PanelService.Instance.InstantiateErrorPanel(response.Message);
                return;
            }
        }
        
        public void CheckEndBattle()
        {
            if (HasAliveUnits(_enemyUnits))
                GameControlService.Instance.FinishBattleLevel(true);
            else if (HasAliveUnits(_playerUnits))
                GameControlService.Instance.FinishBattleLevel(false);
        }

        private bool HasAliveUnits(List<Unit> units)
        {
            return units.Any(unit => !unit.IsDead);
        }

        public void StartTurn()
        {
            _turnInteractor.StartTurn();
        }
    }
}
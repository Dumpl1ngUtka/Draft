using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.Units;
using Battle.Units.Interactors.Reaction;
using Grid.Cells;
using Services.GameControlService;
using Services.GameControlService.GridStateMachine;
using Services.PanelService;

namespace Grid.BattleGrid
{
    public class BattleGridModel : GridModel
    {
        private List<Unit> _playerUnits;
        private List<Unit> _enemyUnits;        
        private List<UnitGridCell> _playerCells;
        private List<UnitGridCell> _enemyCells;
        private UseReactionInteractor _useReactionInteractor;
        private TurnInteractor _turnInteractor;


        public BattleGridModel(GridStateMachine stateMachine) : base(stateMachine)
        {
            _useReactionInteractor = new UseReactionInteractor();
        }
        
        public void AddCells(List<UnitGridCell> playerCells, List<UnitGridCell> enemyCells)
        {
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
            if (!HasAliveUnits(_enemyUnits))
                StateMachine.ChangeGrid(StateMachine.DungeonGrid);
            else if (!HasAliveUnits(_playerUnits))
                StateMachine.ChangeGrid(StateMachine.PathMapGrid);
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
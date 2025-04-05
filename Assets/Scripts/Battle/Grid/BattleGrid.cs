using System;
using System.Collections.Generic;
using Battle.InfoPanel;
using Battle.Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battle.Grid
{
    public class BattleGrid : Grid
    {
        [SerializeField] private CardInfoPanel _cardInfoPrefab;
        [SerializeField] private GridCell[] _enemyCells;

        public override void Initialize(GameManager manager)
        {
            base.Initialize(manager);
            InitiateCells(9, _enemyCells);
        }

        public void Fill(List<PlayerUnit> playerUnits, List<Enemy> enemUnits)
        {
            var index = 0;
            foreach (var cell in PlayerCells)
            {
                cell.AddUnit(playerUnits[index++], 0);
            }
            index = 0;
            foreach (var cell in _enemyCells)
            {
                cell.AddUnit(enemUnits[index++], 1);
            }
            NewTurn();
        }

        public void EndTurn()
        {
            foreach (var enemy in _enemyCells)
            {
                //Dragged(enemy, )
            }
        }
        
        public void NewTurn()
        {
            foreach (var cell in PlayerCells)
            {
                var powerValue = Random.Range(1, 7);
                cell.SetPower(powerValue);
            }
        }

        protected override void Clicked(GridCell cell)
        {
            ShowCardInfo(cell.Unit);
        }

        private void ShowCardInfo(Unit cellUnit)
        {
            _cardInfoPrefab.Instantiate(cellUnit);
            _cardInfoPrefab.Render(cellUnit);
        }

        protected override void Dragged(GridCell from, GridCell to)
        {
            if (from.IsUnitFinished)
                return;
            
            if (from.Unit.Abilities[0].TryUseAbility(from,to, PlayerCells))
            {
                from.Unit.Reaction.SetReaction(from, PlayerCells);
                from.IsUnitFinished = true;
            }
        }
    }
}
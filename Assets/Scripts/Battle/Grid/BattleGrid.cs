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

        public void Fill(List<Unit> units)
        {
            var index = 0;
            foreach (var cell in Cells)
            {
                cell.AddUnit(units[index++]);
            }
            NewTurn();
        }

        public void NewTurn()
        {
            foreach (var cell in Cells)
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
            from.IsUnitFinished = true;
            from.Unit.Race.Reaction.SetReaction(from, Cells);
            from.Unit.Class.Abilities[0].SetAbility(from,to, Cells);
        }
    }
}
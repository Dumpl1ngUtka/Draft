using System;
using System.Collections.Generic;
using Battle.InfoPanel;
using Battle.Units;
using UnityEngine;

namespace Battle.Grid.CardParameter
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
            throw new System.NotImplementedException();
        }
    }
}
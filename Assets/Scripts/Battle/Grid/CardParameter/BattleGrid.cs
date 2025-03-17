using System;
using Battle.InfoPanel;
using Battle.Units;
using UnityEngine;

namespace Battle.Grid.CardParameter
{
    public class BattleGrid : Grid
    {
        [SerializeField] private CardInfoPanel _cardInfoPrefab;

        private void Start()
        {
            Fill();
        }

        private void Fill()
        {
            
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
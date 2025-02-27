using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Grid
{
    public class GridFiller : MonoBehaviour
    {
        [SerializeField] private GridCell[] _gridCells;
        [SerializeField] private GridCell _gridCellPrefab;
        [SerializeField] private float _offsetY;
        [SerializeField] private float _xMaxDistance;
        private int _cardIndex = 0;
        private Grid _grid;

        public GridCell[] Fill(Grid grid, GridScheme gridScheme)
        {
            _grid = grid;
            _cardIndex = 0;
            
            FillLine(0, gridScheme.FirstLineCount, _offsetY);
            FillLine(1, gridScheme.SecondLineCount, 0);
            FillLine(2, gridScheme.ThirdLineCount, -_offsetY);

            return _gridCells;
        }

        private void FillLine( int lineIndex, int cardCount, float offsetY)
        {
            var distanceBetweenCards = (_xMaxDistance * 2 - cardCount * _gridCellPrefab.RectTransform.rect.width)/ 
                                       (cardCount + 1);
            
            for (var i = 0; i < cardCount; i++)
            {
                var card = _gridCells[_cardIndex];
                var xPos = -_xMaxDistance + (i+1) * distanceBetweenCards + 
                           ((i+1) * 2 - 1) * _gridCellPrefab.RectTransform.rect.width / 2;
                card.transform.localPosition = new Vector2(xPos, offsetY);
                card.Init(_cardIndex++, lineIndex, _grid);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Grid
{
    public abstract class Grid : MonoBehaviour
    {
        [SerializeField] private int _startIndex = 0;
        [SerializeField] protected GridCell[] Cells;
        private readonly int _lineCount = 3;
        private readonly int _columnCount = 3;

        protected GameManager Manager;
        
        public void Initialize(GameManager manager)
        {
            Manager = manager;
            InitiateCells();
        }
        
        private void InitiateCells()
        {
            if (_lineCount * _columnCount > Cells.Length)
                throw new Exception("Cell count is too small");
            
            var cardIndex = _startIndex;

            for (int line = 0; line < _lineCount; line++)
            {
                for (int column = 0; column < _columnCount; column++)
                {
                    var cell = Cells[line * _lineCount + column];
                    cell.Init(cardIndex++, line, this);
                    cell.Dragged += Dragged;
                    cell.Clicked += Clicked;
                }
            }
        }

        protected abstract void Clicked(GridCell cell);

        protected abstract void Dragged(GridCell from, GridCell to);

    }
}

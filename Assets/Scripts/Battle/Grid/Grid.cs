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
        [SerializeField] protected GridCell[] PlayerCells;
        private readonly int _lineCount = 3;
        private readonly int _columnCount = 3;

        protected GameManager Manager;
        
        public virtual void Initialize(GameManager manager)
        {
            Manager = manager;
            InitiateCells(_startIndex, PlayerCells);
        }
        
        protected void InitiateCells(int startIndex, GridCell[] cells)
        {
            if (_lineCount * _columnCount > cells.Length)
                throw new Exception("Cell count is too small");
            
            var cardIndex = startIndex;

            for (int line = 0; line < _lineCount; line++)
            {
                for (int column = 0; column < _columnCount; column++)
                {
                    var cell = cells[line * _lineCount + column];
                    cell.Init(cardIndex++, line, column ,this);
                    cell.Dragged += Dragged;
                    cell.Clicked += Clicked;
                }
            }
        }

        protected abstract void Clicked(GridCell cell);

        protected abstract void Dragged(GridCell from, GridCell to);

    }
}

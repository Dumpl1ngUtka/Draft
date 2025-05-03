using System;
using System.Collections.Generic;
using Grid.Cells;
using UnityEngine;

namespace Grid.SelectDungeonGrid
{
    public class SelectDungeonGridView : GridView
    {
        [SerializeField] private List<DungeonCell> _cells;
        [SerializeField] private Transform _container;
        public List<DungeonCell> Cells => _cells;
        
        public void ControlCellSize()
        {
            var screenWidth = Screen.width;
            var cellSize = screenWidth / 4;
            var padding = screenWidth * 0.05f;
            var cellPos = new List<float>()
            {
                screenWidth / 2,
                cellSize/2 + padding,
                screenWidth / 2,
                screenWidth - cellSize/2 - padding,
            };

            var index = 0;
            foreach (DungeonCell cell in _cells)
            {
                cell.RectTransform.sizeDelta = new Vector2(cellSize, cellSize);
                var xPos = cellPos[index++ % cellPos.Count];
                var yPos = (cellSize + padding) * index;
                cell.RectTransform.position = new Vector2(xPos, yPos);
            }
        }
    }
}
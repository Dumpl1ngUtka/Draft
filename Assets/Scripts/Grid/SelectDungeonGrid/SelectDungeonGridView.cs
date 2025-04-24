using System;
using System.Collections.Generic;
using Grid.Cells;
using Services.PanelService;
using UnityEngine;

namespace Grid.SelectDungeonGrid
{
    public class SelectDungeonGridView : GridView
    {
        [SerializeField] private List<DungeonCell> _cells;
        public List<DungeonCell> Cells => _cells;

        public void ShowInfoPanel(DungeonCell cell)
        {
        }
    }
}
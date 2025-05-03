using System.Collections.Generic;
using Battle.Units;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Grid.DraftGrid
{
    public class DraftGridView : GridView
    {
        [SerializeField] private SelectUnitsMenu.SelectUnitsPanel _selectUnitsPanel;
        
        public void ShowSelectMenu(List<Unit> units)
        {
            _selectUnitsPanel.SetActive(true);
            _selectUnitsPanel.RenderUnits(units);
        }

        public void HideSelectMenu()
        {
            _selectUnitsPanel.SetActive(false);
        }
    }
}
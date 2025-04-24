using System.Collections.Generic;
using Battle.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Grid.DraftGrid
{
    public class DraftGridView : GridView
    {
        [FormerlySerializedAs("_selectUnitsMenu")] [SerializeField] private SelectUnitsMenu.SelectUnitsPanel _selectUnitsPanel;
        
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
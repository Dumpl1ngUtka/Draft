using System;
using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class DuckIconInteractor : GridCellInteractor
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _noneSprite;
        
        protected override void ActiveChanged(bool isActive)
        {
            _icon.gameObject.SetActive(isActive);
        }

        protected override void UpdateInfo(Unit unit)
        {
            _icon.sprite = unit == null? _noneSprite : unit.Icon;
        }

        public void Display(Unit unit)
        {
            
        }
    }
}
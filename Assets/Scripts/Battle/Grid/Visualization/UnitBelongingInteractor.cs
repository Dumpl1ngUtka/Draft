using System;
using Battle.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class UnitBelongingInteractor : GridCellInteractor
    {
        private Sprite _noneIcon;
        
        [SerializeField] private Image _covenantImage;
        [SerializeField] private Image _raceImage;
        
        protected override void ActiveChanged(bool isActive)
        {
            _covenantImage.enabled = isActive;
            _raceImage.enabled = isActive;
        }

        protected override void UpdateInfo(Unit unit)
        {
            if (_noneIcon == null)
                _noneIcon = Resources.Load<Sprite>("Sprites/None");
            
            _covenantImage.sprite = unit.Covenant == null ? _noneIcon : unit.Covenant.Icon;
            _raceImage.sprite = unit.Race == null ? _noneIcon : unit.Race.Icon;
        }
    }
}
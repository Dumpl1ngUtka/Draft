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
        [SerializeField] private Image _covenantImage;
        [SerializeField] private Image _raceImage;
        private Sprite _noneIcon;

        public override void UpdateInfo()
        {
            base.UpdateInfo();
            if (_noneIcon == null)
                _noneIcon = Resources.Load<Sprite>("Sprites/None");
            
            if (_covenantImage != null)
                _covenantImage.sprite = Unit.Covenant == null ? _noneIcon : Unit.Covenant.Icon;
            if (_raceImage != null)
                _raceImage.sprite = Unit.Race == null ? _noneIcon : Unit.Race.Icon;
        }
    }
}
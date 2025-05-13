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

        protected override void ActiveChanged(bool isActive)
        {
            _covenantImage?.gameObject.SetActive(isActive);
            _raceImage?.gameObject.SetActive(isActive);
        }

        protected override void UpdateInfo(Unit unit)
        {
            if (_noneIcon == null)
                _noneIcon = Resources.Load<Sprite>("Sprites/None");
            
            if (_covenantImage != null)
                _covenantImage.sprite = unit.Covenant == null ? _noneIcon : unit.Covenant.Icon;
            if (_raceImage != null)
                _raceImage.sprite = unit.Race == null ? _noneIcon : unit.Race.Icon;
        }
    }
}
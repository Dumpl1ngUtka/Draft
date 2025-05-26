using System;
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

        protected override void UpdateInfo()
        {
            if (_noneIcon == null)
                _noneIcon = Resources.Load<Sprite>("Sprites/None");
            
            if (_covenantImage != null)
                _covenantImage.sprite = Unit.Covenant == null ? _noneIcon : Unit.Covenant.Icon;
            if (_raceImage != null)
                _raceImage.sprite = Unit.Race == null ? _noneIcon : Unit.Race.Icon;
        }

        protected override void SetActive(bool isActive)
        {
            _covenantImage?.gameObject.SetActive(isActive);
            _raceImage?.gameObject.SetActive(isActive);
        }
    }
}
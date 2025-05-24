using System;
using Battle.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class DiceInteractor : GridCellInteractor
    {
        private Sprite _noneIcon;
        [SerializeField] private Image _diceIcon;
        [SerializeField] private TMP_Text _additionalValueText;
        [SerializeField] private Image _additionalValueImage;

        protected override void UpdateInfo()
        {
            if (_diceIcon == null)
                return;
            
            if (!Unit.IsReady)
            {
                if (_noneIcon == null)
                    _noneIcon = Resources.Load<Sprite>("Sprites/None");
                
                _diceIcon.sprite = _noneIcon;
                return;
            }
            
            var value = Unit.DicePower + 1;
            _diceIcon.sprite = Resources.Load<Sprite>("Sprites/Dice/" + value);
        }

        protected override void SetActive(bool isActive)
        {
            _diceIcon.gameObject.SetActive(isActive);
            _additionalValueText.gameObject.SetActive(isActive);
            _additionalValueImage.gameObject.SetActive(isActive);
        }

        public void SetActiveAdditionalValue(bool isActive)
        {
            _additionalValueImage?.gameObject.SetActive(isActive);
            _additionalValueText?.gameObject.SetActive(isActive);
        }

        public void RenderAdditionalValue(int value)
        {
            if (_additionalValueImage == null || _additionalValueText == null)
                return;
            
            var isPositive = value > 0;
            _additionalValueText.text = (isPositive ? "+" : "") + value;
            _additionalValueImage.color = isPositive ? Color.green : Color.red;
        }
    }
}

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
        
        protected override void ActiveChanged(bool isActive)
        {
            _diceIcon.gameObject.SetActive(isActive);
            SetActiveAdditionalValue(false);
        }

        protected override void UpdateInfo(Unit unit)
        {
            if (!unit.IsReady)
            {
                _diceIcon.sprite = Resources.Load<Sprite>("Sprites/None");
                return;
            }
            
            var value = unit.DicePower + 1;
            _diceIcon.sprite = Resources.Load<Sprite>("Sprites/Dice/" + value);
        }

        public void SetActiveAdditionalValue(bool isActive)
        {
            _additionalValueImage.gameObject.SetActive(isActive);
            _additionalValueText.gameObject.SetActive(isActive);
        }

        public void RenderAdditionalValue(int value)
        {
            var isPositive = value > 0;
            _additionalValueText.text = (isPositive ? "+" : "") + value;
            _additionalValueImage.color = isPositive ? Color.green : Color.red;
        }
    }
}

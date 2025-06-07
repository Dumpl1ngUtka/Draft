using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class DiceInteractor : GridCellInteractor
    {
        private Sprite _noneIcon;
        [SerializeField] private Image _diceIcon;
        [SerializeField] private Image _upgradeIcon;
        [SerializeField] private Image _downgradedIcon;
        [Header("Colors")]
        [SerializeField] private Color _upgradeColor;
        [SerializeField] private Color _downgradedColor;

        protected override void UpdateInfo()
        {
            if (_diceIcon == null)
                return;
            
            _diceIcon.gameObject.SetActive(Unit.Stats.IsReady);
            RenderAdditionalValue(0);
            
            _diceIcon.sprite = Resources.Load<Sprite>("Sprites/Dice/Dice" + (Unit.DiceInteractor.DicePower + 1));
        }

        protected override void SetActive(bool isActive)
        {
            _diceIcon?.gameObject.SetActive(isActive);
            _upgradeIcon?.gameObject.SetActive(isActive);
            _downgradedIcon?.gameObject.SetActive(isActive);
        }
        
        public void RenderAdditionalValue(int value)
        {
            if (_upgradeIcon == null || _downgradedIcon == null)
                return;
            
            if (!Unit.Stats.IsReady)
                return;

            if (value == 0)
            {
                _upgradeIcon.gameObject.SetActive(false);
                _downgradedIcon.gameObject.SetActive(false);
                _diceIcon.color = Color.white;
                return;
            }
            var isPositive = value > 0;
            _upgradeIcon.gameObject.SetActive(isPositive);
            _downgradedIcon.gameObject.SetActive(!isPositive);
            _diceIcon.color = isPositive ? _upgradeColor : _downgradedColor;
        }
    }
}

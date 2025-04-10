using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Visualization
{
    public class DiceRenderer : MonoBehaviour
    {  
        [SerializeField] private Image _diceIcon;
        [SerializeField] private TMP_Text _additionalValueText;
        [SerializeField] private Image _additionalValueImage;

        public void SetActive(bool active)
        {
            _diceIcon.gameObject.SetActive(active);
            SetAdditionalValue(false);
        }

        public void SetDiceValue(int value)
        {
            _diceIcon.sprite = Resources.Load<Sprite>("Sprites/Dice/" + value);
        }

        public void SetAdditionalValue(bool isActive, int value = 0)
        {
            _additionalValueImage.gameObject.SetActive(isActive);
            _additionalValueText.gameObject.SetActive(isActive);
            if (value == 0) return;

            var isPositive = value > 0;
            _additionalValueText.text = (isPositive ? "+" : "") + value;
            _additionalValueImage.color = isPositive ? Color.green : Color.red;
        }
    }
}

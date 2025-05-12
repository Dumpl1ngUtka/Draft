using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid.CardParameter.GraduationParameter
{
    public class GraduationParameter : MonoBehaviour
    {
        [SerializeField] private Image _upgradeIcon;
        [SerializeField] private Image _downgradeIcon;
        
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetStatus(bool isUpgrade, Sprite icon)
        {
            if (isUpgrade)
            {
                _upgradeIcon.sprite = icon;
            }
            else
            {
                _downgradeIcon.sprite = icon;
            }
            _upgradeIcon.gameObject.SetActive(isUpgrade);
            _downgradeIcon.gameObject.SetActive(!isUpgrade);
        }
    }
}
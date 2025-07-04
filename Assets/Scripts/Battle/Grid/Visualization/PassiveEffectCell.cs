using Battle.PassiveEffects;
using Services.PanelService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid.Visualization
{
    public class PassiveEffectCell : MonoBehaviour
    {
        private string _key;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _value;

        public void Init(PassiveEffect effect)
        {
            _key = effect.DBKey;
            _icon.color = effect.Color;
            _icon.sprite = effect.Icon;
            _value.text = effect.TurnCount >= 10? "" : effect.TurnCount.ToString();
        }

        public void ShowInfo()
        {
            PanelService.Instance.InstantiateInfoPanel(_key);
        }
    }
}
using System;
using Battle.Grid.CardParameter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class HealthInteractor : GridCellInteractor
    {
        [SerializeField] private TMP_Text _value;
        [SerializeField] private Image _bar;
        [Header("Colors")]
        [SerializeField] private Color _healthColor;
        [SerializeField] private Color _armorColor;
        
        protected override void UpdateInfo()
        {
            if (_value == null || _bar == null)
                return;
            var healthText = "";
            var armorValue = Unit.Stats.Armor.Value;
            if (armorValue > 0) 
                healthText = armorValue.ToString() + " + ";
            healthText += Unit.Stats.CurrentHealth.Value + " / " + Unit.Stats.MaxHealth.Value;
            _value.text = healthText;

            _bar.color = armorValue > 0 ? _armorColor : _healthColor;
            _bar.fillAmount = Unit.Stats.CurrentHealth.Value / (float)Unit.Stats.MaxHealth.Value;
        }

        protected override void SetActive(bool isActive)
        {
            _value?.gameObject.SetActive(isActive);
            _bar?.gameObject.SetActive(isActive);
        }
    }
}
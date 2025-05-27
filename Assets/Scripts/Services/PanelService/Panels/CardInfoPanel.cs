using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Services.PanelService.Panels
{
    public class CardInfoPanel : Panel
    {
        [Header("Card Info")]
        [SerializeField] private Image _duckIcon;
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private TMP_Text _classField;
        [SerializeField] private TMP_Text _raceField;
        [SerializeField] private TMP_Text _covenantField;
        [Header("Parameter")]
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _strength;
        [SerializeField] private TMP_Text _dexterity;
        [SerializeField] private TMP_Text _intelligence;
        
        public void Init(Unit unit)
        {
            Render(unit);
            SetRandomRotation();
        }

        public void Render(Unit unit)
        {
            _duckIcon.sprite = unit.Icon;
            _nameField.text = unit.Name;
            _raceField.text = unit.Race?.Name;
            _classField.text = unit.Class?.Name;
            _covenantField.text = unit.Covenant?.Name;
            _health.text = unit.Stats.HealthAttribute.Value.ToString();
            _strength.text = unit.Stats.StrengthAttribute.Value.ToString();
            _dexterity.text = unit.Stats.DexterityAttribute.Value.ToString();
            _intelligence.text = unit.Stats.IntelligenceAttribute.Value.ToString();
        }
    }
}
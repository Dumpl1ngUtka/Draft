using Battle.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.PanelService.Panels
{
    public class CardInfoPanel : InfoPanel
    {
        [Header("Card Info")]
        [SerializeField] private RectTransform _cardInfoPanel;
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
        private float _maxRotation = 3;
        
        public void Init(Unit unit)
        {
            Render(unit);
            SetRandomRotation();
        }

        private void SetRandomRotation()
        {
            _cardInfoPanel.rotation = Quaternion.Euler(0, 0, Random.Range(-_maxRotation, _maxRotation));
        }

        public void Render(Unit unit)
        {
            _duckIcon.sprite = unit.Class.Icon;
            _nameField.text = unit.Name;
            _raceField.text = unit.Race.Name;
            _classField.text = unit.Class.Name;
            _covenantField.text = unit.Covenant.Name;
            _health.text = unit.Attributes.Health.ToString();
            _strength.text = unit.Attributes.Strength.ToString();
            _dexterity.text = unit.Attributes.Dexterity.ToString();
            _intelligence.text = unit.Attributes.Intelligence.ToString();
        }
    }
}
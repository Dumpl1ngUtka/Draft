using Battle.Units;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle.SelectUnitsMenu
{
    public class SelectUnitsCell : MonoBehaviour, IPointerClickHandler
    {
        
        [SerializeField] private TMP_Text _classField;
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private TMP_Text _levelField;
        [SerializeField] private Image _outlineImage;
        [Header("Icons")]
        [SerializeField] private Image _raceIcon;
        [SerializeField] private Image _covenantIcon;
        [Header("Attributes")]
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _strengthText;
        [SerializeField] private TMP_Text _dexterityText;
        [SerializeField] private TMP_Text _intelligenceText;
        private SelectUnitsMenu _selectUnitsMenu;
        
        public Unit Unit { get; private set; } 
            
        public void Init(SelectUnitsMenu menu ,Unit unit)
        {
            Unit = unit;
            
            _selectUnitsMenu = menu;
            _levelField.text = unit.Level.ToString();
            _classField.text = unit.Class.Name;
            _nameField.text = unit.Name;
            _raceIcon.sprite = unit.Race.Icon;
            _covenantIcon.sprite = unit.Covenant.Icon;
            
            _healthText.text = unit.Attributes.Health.ToString();
            _strengthText.text = unit.Attributes.Strength.ToString();
            _dexterityText.text = unit.Attributes.Dexterity.ToString();
            _intelligenceText.text = unit.Attributes.Intelligence.ToString();
        }

        public void SetOutline(bool isSelected)
        {
            _outlineImage.enabled = isSelected;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _selectUnitsMenu.SelectUnitCell(this);
        }
    }
}
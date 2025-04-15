using Battle.Grid.CardParameter;
using Battle.Units;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Battle.SelectUnitsMenu
{
    public class SelectUnitsCell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _duckIcon;
        [SerializeField] private TMP_Text _classField;
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private CircleParameter _levelField;
        [SerializeField] private Image _outlineImage;
        [Header("Icons")]
        [SerializeField] private Image _raceIcon;
        [SerializeField] private Image _covenantIcon;
        [Header("Attributes")]
        [SerializeField] private CircleParameter _health;
        [SerializeField] private CircleParameter _strength;
        [SerializeField] private CircleParameter _dexterity;
        [SerializeField] private CircleParameter _intelligence;
        private SelectUnitsMenu _selectUnitsMenu;
        
        public Unit Unit { get; private set; } 
            
        public void Init(SelectUnitsMenu menu ,Unit unit)
        {
            Unit = unit;
            
            _duckIcon.sprite = unit.Class.Icon;
            _selectUnitsMenu = menu;
            _classField.text = unit.Class.Name;
            _nameField.text = unit.Name;
            _raceIcon.sprite = unit.Race.Icon;
            _covenantIcon.sprite = unit.Covenant.Icon;
            
            _health.Render(unit.Attributes.Health);
            _strength.Render(unit.Attributes.Strength);
            _dexterity.Render(unit.Attributes.Dexterity);
            _intelligence.Render(unit.Attributes.Intelligence);
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
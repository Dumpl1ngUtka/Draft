using Battle.Grid.CardParameter;
using Battle.Units;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Grid.DraftGrid.SelectUnitsMenu
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
        private SelectUnitsPanel _selectUnitsPanel;
        
        public Unit Unit { get; private set; } 
            
        public void Init(SelectUnitsPanel panel ,Unit unit)
        {
            Unit = unit;
            
            _duckIcon.sprite = unit.Class.Icon;
            _selectUnitsPanel = panel;
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
            _selectUnitsPanel.SelectUnitCell(this);
        }
    }
}
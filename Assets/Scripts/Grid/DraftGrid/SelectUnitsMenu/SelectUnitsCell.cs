using Battle.Grid.CardParameter;
using Battle.Grid.CardParameter.GraduationParameter;
using Battle.Units;
using TMPro;
using Units;
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
        [SerializeField] private Image _outlineImage;
        [Header("Icons")]
        [SerializeField] private Image _raceIcon;
        [SerializeField] private Image _covenantIcon;
        [Header("Attributes")]
        [SerializeField] private GraduationParameterHolder _parameterHolder;
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
            
            _parameterHolder.Render(unit);
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
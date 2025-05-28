using System;
using Battle.Grid.CardParameter.GraduationParameter;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Grid.DraftGrid.SelectUnitsMenu
{
    public class SelectUnitsCell : MonoBehaviour
    {
        [SerializeField] private Image _duckIcon;
        [SerializeField] private TMP_Text _classField;
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private Image _outlineImage;
        [SerializeField] private TMP_Text _cellIndex;
        [Header("Icons")]
        [SerializeField] private Image _raceIcon;
        [SerializeField] private Image _covenantIcon;
        [Header("Attributes")]
        [SerializeField] private GraduationParameterHolder _parameterHolder;
        [SerializeField] private TMP_Text _healthValue;
        [SerializeField] private TMP_Text _strengthValue;
        [SerializeField] private TMP_Text _dexterityValue;
        [SerializeField] private TMP_Text _intelligenceValue;
        [SerializeField] private float _moveSpeed = 10;
        private SelectUnitsPanel _selectUnitsPanel;
        private Unit _unit;
        private Vector2 _delta;
        private Vector2 _center;

        public void Init(int index, int maxIndex, Unit unit)
        {
            _center = new Vector2(Screen.width / 2, Screen.height / 2);
            
            _unit = unit;
            _cellIndex.text = index + "/" + maxIndex;
            _duckIcon.sprite = unit.Class.Icon;
            _classField.text = unit.Class.Name;
            _nameField.text = unit.Name;
            _raceIcon.sprite = unit.Race.Icon;
            _covenantIcon.sprite = unit.Covenant.Icon;

            var unitStats = unit.Stats;
            _healthValue.text = unitStats.HealthAttribute.Value.ToString();
            _strengthValue.text = unitStats.StrengthAttribute.Value.ToString();
            _dexterityValue.text = unitStats.DexterityAttribute.Value.ToString();
            _intelligenceValue.text = unitStats.IntelligenceAttribute.Value.ToString();

            SetRandomRotation();
        }
        
        public void SetDelta(Vector2 delta)
        {
            _delta = delta;
        }

        private void Update()
        {
            transform.position = Vector2.Lerp(transform.position, _center + _delta, 
                Time.deltaTime * _moveSpeed) ;
        }

        private void SetRandomRotation()
        {
            ((RectTransform)transform).rotation = Quaternion.Euler(0, 0, Random.Range(-3f, 3f));
        }
    }
}
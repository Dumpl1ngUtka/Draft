using System;
using Abilities;
using Battle.UseCardReactions;
using Services.SaveLoadSystem;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Grid.DraftGrid.SelectUnitsMenu
{
    public class SelectUnitsCell : MonoBehaviour
    {
        [SerializeField] private Image _duckIcon;
        [SerializeField] private TMP_Text _classField;
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private TMP_Text _cellIndex;
        [Header("Icons")]
        [SerializeField] private Image _raceIcon;
        [SerializeField] private Image _covenantIcon;
        [Header("Attributes")]
        [SerializeField] private TMP_Text _healthValue;
        [SerializeField] private TMP_Text _strengthValue;
        [SerializeField] private TMP_Text _dexterityValue;
        [SerializeField] private TMP_Text _intelligenceValue;
        [Header("Abilities")]
        [SerializeField] private Transform _abilityContainer;
        [SerializeField] private ClickableObject _abilityPrefab;
        [Header("Inventory")]
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private ClickableObject _itemPrefab;
        [Header("Reactions")]
        [SerializeField] private Image _reactionIcon;
        [SerializeField] private TMP_Text _reactionText;
        [Header("Animations")]
        [SerializeField] private float _moveSpeed = 10;
        [SerializeField] private float _rotationValue = 3;
        private Vector2 _delta;
        private Vector2 _center;

        public void Init(int index, int maxIndex, Unit unit)
        {
            _center = new Vector2(Screen.width / 2, Screen.height / 2);
            
            _cellIndex.text = index + "/" + maxIndex;
            _duckIcon.sprite = unit.Class.Icon;
            _classField.text = unit.Class.Name;
            _nameField.text = unit.Name;
            _raceIcon.sprite = unit.Race.Icon;
            _covenantIcon.sprite = unit.Covenant.Icon;
            
            
            InitAttributes(unit.Stats);
            InitAbilities(unit.Abilities);
            InitReaction(unit.Reaction);

            SetRandomRotation();
        }
        
        public void SetDelta(Vector2 delta) => _delta = delta;

        private void Update()
        {
            transform.position = Vector2.Lerp(transform.position, _center + _delta, 
                Time.deltaTime * _moveSpeed) ;
        }

        private void SetRandomRotation()
        {
            ((RectTransform)transform).rotation = Quaternion.Euler(0, 0, Random.Range(-_rotationValue, _rotationValue));
        }

        private void InitAttributes(UnitStats unitStats)
        {
            _healthValue.text = unitStats.HealthAttribute.Value.ToString();
            _strengthValue.text = unitStats.StrengthAttribute.Value.ToString();
            _dexterityValue.text = unitStats.DexterityAttribute.Value.ToString();
            _intelligenceValue.text = unitStats.IntelligenceAttribute.Value.ToString();
        }
        
        private void InitAbilities(AbilitiesHolder abilitiesHolder)
        {
            for (int i = 0; i < abilitiesHolder.AbilityCount; i++)
            {
                var ability = abilitiesHolder.GetAbilityByIndex(i);
                var instance = Instantiate(_abilityPrefab, _abilityContainer);
                if (ability != null)
                    instance.Init(ability.Name, ability.Icon);
                else 
                    instance.Init("empty_ability", GetCubeSpriteByIndex(i));
            }
        }
        
        private void InitReaction(Reaction reaction)
        {
            _reactionIcon.sprite = reaction.Icon;
            _reactionText.text = SaveLoadService.Instance.GetDescriptionByKey(reaction.Key).Description;
        }

        private Sprite GetCubeSpriteByIndex(int index)
        {
            return Resources.Load<Sprite>("Sprites/Dice/Dice" + index);
        }
    }
}
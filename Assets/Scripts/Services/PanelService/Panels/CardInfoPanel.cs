using Abilities;
using Battle.Grid.Visualization;
using Battle.PassiveEffects;
using Battle.UseCardReactions;
using Grid.DraftGrid.SelectUnitsMenu;
using Services.SaveLoadSystem;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Services.PanelService.Panels
{
    public class CardInfoPanel : Panel
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

        [Header("Passive Effects")] 
        [SerializeField] private Transform _container;
        [SerializeField] private PassiveEffectCell _prefab;
        private PassiveEffectsInteractor _passiveEffectsInteractor;
        [Header("Other")]
        [SerializeField] private Sprite _emptySprite;
        private Vector2 _delta;
        private Vector2 _center;

        public void Init(Unit unit)
        {
            _center = new Vector2(Screen.width / 2, Screen.height / 2);
            
            _cellIndex.text = "";
            _duckIcon.sprite = unit.Icon;
            _nameField.text = unit.Name;
            _classField.text = unit.Class != null? unit.Class.Name : "";
            _raceIcon.sprite = unit.Race != null? unit.Race.Icon : _emptySprite;
            _covenantIcon.sprite = unit.Covenant != null? unit.Covenant.Icon : _emptySprite;
            
            InitAttributes(unit.Stats);
            InitAbilities(unit.Abilities);
            InitReaction(unit.Reaction);
            InitEffects(unit);

            SetRandomRotation();
        }
        
        public void Init(Unit unit, int index, int maxIndex)
        {
            Init(unit);
            _cellIndex.text = index + "/" + maxIndex;
        }
        
        public void SetDelta(Vector2 delta) => _delta = delta;

        private void Update()
        {
            transform.position = Vector2.Lerp(transform.position, _center + _delta, 
                Time.deltaTime * _moveSpeed) ;
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
            if (reaction == null)
            {
                _reactionIcon.sprite = _emptySprite;
                _reactionText.text = "";
                return;
            }
            
            _reactionIcon.sprite = reaction.Icon;
            _reactionText.text = SaveLoadService.Instance.GetDescriptionByKey(reaction.Key).Description;
        }
        
        private void InitEffects(Unit unit)
        {
            _passiveEffectsInteractor = new PassiveEffectsInteractor();
            _passiveEffectsInteractor.SetFields(_prefab, _container);
            _passiveEffectsInteractor.Init(unit, null);
            _passiveEffectsInteractor.TryUpdateInfo();
        }

        private Sprite GetCubeSpriteByIndex(int index)
        {
            return Resources.Load<Sprite>("Sprites/Dice/Dice" + index);
        }
    }
}
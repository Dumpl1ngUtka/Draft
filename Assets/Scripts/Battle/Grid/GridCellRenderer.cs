using System;
using Battle.Grid.CardParameter;
using Battle.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid
{
    public class GridCellRenderer : MonoBehaviour
    {
        [SerializeField] private Image _duckIcon;
        [SerializeField] private Parameter _chemistry;
        [SerializeField] private Image _covenantImage;
        [SerializeField] private Image _raceImage;
        [SerializeField] private Parameter _level;
        [SerializeField] private Image _powerDiceImage;
        [Header("Attributes")]
        [SerializeField] private Parameter _health;
        [SerializeField] private Parameter _strength;
        [SerializeField] private Parameter _dexterity;
        [SerializeField] private Parameter _intelligence;
        [Header("Health")]
        [SerializeField] private TMP_Text _armorValue;
        [SerializeField] private TMP_Text _healthValue;
        private GridCell _cell;
        private Sprite _noneIcon;

        private void Awake()
        {
            _cell = GetComponent<GridCell>();
            _noneIcon = Resources.Load<Sprite>("Sprites/None");
        }

        public void SetActive(bool active)
        {
            SetActiveParameters(active);
            _duckIcon.gameObject.SetActive(active);
            _level.gameObject.SetActive(active);
            _chemistry.gameObject.SetActive(active);
            _covenantImage.gameObject.SetActive(active);
            _raceImage.gameObject.SetActive(active);
            _powerDiceImage.gameObject.SetActive(active);
            _armorValue.gameObject.SetActive(active);
            _healthValue.gameObject.SetActive(active);
        }

        public void SetActiveParameters(bool active)
        {
            _health.gameObject.SetActive(active);
            _strength.gameObject.SetActive(active);
            _dexterity.gameObject.SetActive(active);
            _intelligence.gameObject.SetActive(active);
        }
        
        public void Render(Unit unit)
        {
            if (unit.IsDead)
                SetActive(false);
            _duckIcon.sprite = unit.Icon;
            _health?.Render(unit.Attributes.Health);
            _strength?.Render(unit.Attributes.Strength);
            _dexterity?.Render(unit.Attributes.Dexterity);
            _intelligence?.Render(unit.Attributes.Intelligence);
            //_chemistry.Render(unit.Chemistry);
            _covenantImage.sprite = unit.Covenant == null? _noneIcon : unit.Covenant.Icon;
            _raceImage.sprite = unit.Race == null? _noneIcon : unit.Race.Icon;
            _powerDiceImage.sprite = unit.IsReady?
               Resources.Load<Sprite>("Sprites/Dice/" + (unit.DicePower + 1)) :  _noneIcon;
            RenderHealth(unit.Health.CurrentHealth, unit.Health.MaxHealth);
            RenderArmor(unit.Health.ArmorValue);
        }

        public void RenderHealth(int currentHealth, int maxHealth)
        {
            _healthValue.text = currentHealth + "/" + maxHealth;
        }

        public void RenderArmor(int armorValue)
        {
            _armorValue.text = armorValue.ToString();
        }
    }
}

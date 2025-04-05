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

        private void Awake()
        {
            _cell = GetComponent<GridCell>();
        }

        public void SetActive(bool active)
        {
            _duckIcon.gameObject.SetActive(active);
            _level.gameObject.SetActive(active);
            _chemistry.gameObject.SetActive(active);
            _health.gameObject.SetActive(active);
            _strength.gameObject.SetActive(active);
            _dexterity.gameObject.SetActive(active);
            _intelligence.gameObject.SetActive(active);
            _covenantImage.gameObject.SetActive(active);
            _raceImage.gameObject.SetActive(active);
            _powerDiceImage.gameObject.SetActive(active);
            _armorValue.gameObject.SetActive(active);
            _healthValue.gameObject.SetActive(active);
        }

        public void Render(PlayerUnit playerUnit)
        {
            Render(playerUnit as Unit);
            _chemistry.Render(playerUnit.Chemistry);
            _covenantImage.sprite = playerUnit.Covenant.Icon;
            _raceImage.sprite = playerUnit.Race.Icon;
        }
        
        public void Render(Unit unit)
        {
            _duckIcon.sprite = unit.Icon;
            _health?.Render(unit.Attributes.Health);
            _strength?.Render(unit.Attributes.Strength);
            _dexterity?.Render(unit.Attributes.Dexterity);
            _intelligence?.Render(unit.Attributes.Intelligence);
            if (_cell.IsUnitFinished) 
                _powerDiceImage.sprite = Resources.Load<Sprite>("Sprites/None");
            else
                _powerDiceImage.sprite = Resources.Load<Sprite>("Sprites/Dice/" + _cell.DicePower);
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

using Battle.Grid.CardParameter;
using Battle.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid
{
    public class GridCellRenderer : MonoBehaviour
    {
        [SerializeField] private Parameter _chemistry;
        [SerializeField] private Image _covenantImage;
        [SerializeField] private Image _raceImage;
        [SerializeField] private Parameter _level;
        [Header("Attributes")]
        [SerializeField] private Parameter _health;
        [SerializeField] private Parameter _strength;
        [SerializeField] private Parameter _dexterity;
        [SerializeField] private Parameter _intelligence;

        public void SetActive(bool active)
        {
            _level.gameObject.SetActive(active);
            _chemistry.gameObject.SetActive(active);
            _health.gameObject.SetActive(active);
            _strength.gameObject.SetActive(active);
            _dexterity.gameObject.SetActive(active);
            _intelligence.gameObject.SetActive(active);
            _covenantImage.gameObject.SetActive(active);
            _raceImage.gameObject.SetActive(active);
        }
        
        public void Render(Unit unit)
        {
            _level.Render(unit.Level / 5) ;
            _chemistry.Render(unit.Chemestry);
            _covenantImage.sprite = unit.Covenant.Icon;
            _raceImage.sprite = unit.Race.Icon;
            _health?.Render(unit.Attributes.Health);
            _strength?.Render(unit.Attributes.Strength);
            _dexterity?.Render(unit.Attributes.Dexterity);
            _intelligence?.Render(unit.Attributes.Intelligence);
        }
    }
}

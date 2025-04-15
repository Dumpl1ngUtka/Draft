using Battle.Grid.CardParameter;
using Battle.Units;
using TMPro;
using UnityEngine;

namespace Battle.InfoPanel
{
    public class CardInfoPanel : InfoPanel
    {
        [SerializeField] private TMP_Text _name;
        [Header("Parameter")]
        [SerializeField] private CircleParameter _health;
        [SerializeField] private CircleParameter _strength;
        [SerializeField] private CircleParameter _dexterity;
        [SerializeField] private CircleParameter _intelligence;
        
        public void Instantiate(Unit unit)
        {
            var canvas = FindFirstObjectByType(typeof(Canvas)) as Canvas;
            if (canvas != null)
            {
                var panel = Instantiate(this, canvas.transform);
                panel.Render(unit);
            }
        }
        
        public void Render(Unit unit)
        {
            _name.text = unit.Name;
            _health.Render(unit.Attributes.Health);
            _strength.Render(unit.Attributes.Strength);
            _dexterity.Render(unit.Attributes.Dexterity);
            _intelligence.Render(unit.Attributes.Intelligence);
        }
    }
}
using System;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid.CardParameter.GraduationParameter
{
    public class GraduationParameterHolder : MonoBehaviour
    {
        [SerializeField] private GraduationParameter _health;
        [SerializeField] private GraduationParameter _strength;
        [SerializeField] private GraduationParameter _dexterity;
        [SerializeField] private GraduationParameter _intelligence;
        [SerializeField] private GridLayoutGroup _container;
        [SerializeField] private int _midLevel = 5;
        private Sprite[] _graduationSprites;
        public void Render(Unit unit)
        {
            var unitStats = unit.Stats;
            RenderParameter(unitStats.HealthAttribute.Value, _health);
            RenderParameter(unitStats.StrengthAttribute.Value, _strength);
            RenderParameter(unitStats.DexterityAttribute.Value, _dexterity);
            RenderParameter(unitStats.IntelligenceAttribute.Value, _intelligence);
        }
        
        private void Awake()
        {
            _graduationSprites = Resources.LoadAll<Sprite>($"Sprites/Graduation");
        }

        private void RenderParameter(int attributeValue, GraduationParameter parameter)
        {
            var delta = attributeValue - _midLevel;
            var isUpgrage = delta > 0;
            var absoluteDelta = Math.Abs(delta);
            
            if (absoluteDelta <= 1)
            {
                parameter.SetActive(false);
            }
            else
            {
                parameter.SetActive(true);
                parameter.SetStatus(isUpgrage, absoluteDelta <= 3 ?
                    _graduationSprites[0] : _graduationSprites[^1]);
            }
        }
    }
}
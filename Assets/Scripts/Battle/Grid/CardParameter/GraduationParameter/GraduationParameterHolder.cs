using System;
using Battle.Units;
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
        private int _graduationSpriteLen;

        public void Render(Unit unit)
        {
            var attributes = unit.Attributes;
            RenderParameter(attributes.Health, _health);
            RenderParameter(attributes.Strength, _strength);
            RenderParameter(attributes.Dexterity, _dexterity);
            RenderParameter(attributes.Intelligence, _intelligence);
        }
        
        private void Awake()
        {
            _graduationSprites = Resources.LoadAll<Sprite>($"Sprites/Graduation");
            _graduationSpriteLen = _graduationSprites.Length;
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
                parameter.SetStatus(isUpgrage, absoluteDelta <= 3 ? _graduationSprites[0] : _graduationSprites[^1]);
            }
        }
    }
}
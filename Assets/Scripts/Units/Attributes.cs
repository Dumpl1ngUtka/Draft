using System;
using UnityEngine;
using Random = System.Random;

namespace Units
{
    [Serializable]
    public class Attributes
    {
        private const int _maxLevel = 10;
        private const int _skillCount = 4;

        [SerializeField] private int _health;
        [SerializeField] private int _dexterity;
        [SerializeField] private int _strength;
        [SerializeField] private int _intelligence;

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
        public int Dexterity
        {
            get { return _dexterity; }
            set { _dexterity = value; }
        }
        public int Strength
        {
            get { return _strength; }
            set { _strength = value; }
        }

        public int Intelligence
        {
            get { return _intelligence; }
            set { _intelligence = value; }
        }

        public Attributes(int skillPoints, Random random = null)
        {
            random ??= new Random();
            var skillLevels = new int[_skillCount];
            for (int i = 0; i < _skillCount; i++)
                skillPoints -= 1;

            for (int i = 0; i < _skillCount - 1; i++)
            {
                skillLevels[i] = 1;
                var availablePoints = (_skillCount - 1 - i) * _maxLevel;
                for (int j = i + 1; j < _skillCount; j++)
                    availablePoints -= 1;
                var lowerRangeBorder = Mathf.Max(skillPoints - availablePoints, 0);

                var upperRangeBorder = Mathf.Min(_maxLevel - 1, skillPoints);

                var additionalPoints = random.Next(lowerRangeBorder, upperRangeBorder + 1);
                skillLevels[i] += additionalPoints;
                skillPoints -= additionalPoints;
            }

            Health = skillLevels[0];
            Dexterity = skillLevels[1];
            Strength = skillLevels[2];
            Intelligence = 1 + skillPoints;
        }

        public Attributes(int health, int dexterity, int strength, int intelligence)
        {
            Health = health;
            Dexterity = dexterity;
            Strength = strength;
            Intelligence = intelligence;
        }
    }
}

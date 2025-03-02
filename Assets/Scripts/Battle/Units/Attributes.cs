using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battle.Units
{
    [Serializable]
    public class Attributes
    {
        private const int maxLevel = 20;

        private int _health;
        private int _dexterity;
        private int _strength;
        private int _intelligence;

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

        public Attributes(int skillPoints)
        {
            var skillCount = 4;

            var skillLevels = new int[skillCount];
            for (int i = 0; i < skillCount; i++)
                skillPoints -= 1;

            for (int i = 0; i < skillCount - 1; i++)
            {
                skillLevels[i] = 1;
                var availablePoints = (skillCount - 1 - i) * maxLevel;
                for (int j = i + 1; j < skillCount; j++)
                    availablePoints -= 1;
                var lowerRangeBorder = Mathf.Max(skillPoints - availablePoints, 0);

                var upperRangeBorder = Mathf.Min(maxLevel - 1, skillPoints);

                var additionalPoints = Random.Range(lowerRangeBorder, upperRangeBorder + 1);
                skillLevels[i] += additionalPoints;
                skillPoints -= additionalPoints;
            }

            Health = skillLevels[0];
            Dexterity = skillLevels[1];
            Strength = skillLevels[2];
            Intelligence = 1 + skillPoints;
        }
        
    }
}

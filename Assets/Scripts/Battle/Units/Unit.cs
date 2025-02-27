using System;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

namespace Battle.Units
{
    public class Unit
    {
        private const int _maxChem = 10;
        
        public string Name { get; private set; }
        public Class Class { get; private set; }
        public Race Race { get; private set; } 
        public Covenant Covenant { get; private set; }
        public int Chemestry { get; private set; }
        public int Level { get; private set; }
       
        public Action ParametersChanged;
        public bool IsMaxChem => Chemestry == _maxChem;
        
        public Unit(Class unitClass, int level)
        {
            Class = unitClass; 
            Race = Class.Races[Random.Range(0, Class.Races.Length)];
            Name = Race.AvailableNames[Random.Range(0, Race.AvailableNames.Length)];
            Covenant = Race.AvailableCovenants[Random.Range(0, Race.AvailableCovenants.Length)];
            Level = level;
        }

        public void SetChemestry(int chemestry)
        {
            Chemestry = chemestry >_maxChem ? _maxChem : chemestry;
            ParametersChanged?.Invoke();
        }
    }
}
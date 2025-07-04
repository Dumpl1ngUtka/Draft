using System;

namespace Services.SaveLoadSystem
{
    [Serializable]
    public class BelongingData
    {
        public string[] AvailableUnitRaces;
        public string[] AvailableUnitClasses;
        public string[] AvailableUnitCovenants;

        public BelongingData()
        {
            AvailableUnitRaces = new string[4]
            {
                "Elf", "Gnome", "Human", "Orc"
            };
            AvailableUnitClasses = new string[7]
            {
                "Bard", "Warrior", "Wizard", "Ranger", "Thief", "Tank", "Paladin"
            };
            AvailableUnitCovenants = new string[6]
            {
                "Red1", "Red2", "Blue1", "Blue2", "Yellow1", "Yellow2"
            };
        }
        
        public BelongingData(string[] rases, string[] classes, string[] covenants)
        {
            AvailableUnitRaces = rases;
            AvailableUnitClasses = classes;
            AvailableUnitCovenants = covenants;
        }
    }
}
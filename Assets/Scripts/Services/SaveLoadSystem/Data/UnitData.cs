using System;
using Items;
using Units;

namespace Services.SaveLoadSystem
{
    [Serializable]
    public class UnitData
    {
        public string ClassName;
        public string RaceName;
        public string CovenantShortName;
        public int HealthValue;
        public GridPosition Position;
        public Attributes Attributes;
        public Item[] Items;

        public UnitData(
            string className,
            string raceName,
            string covenantShortName,
            int randomSeed,
            int healthValue,
            GridPosition position,
            Attributes attributes,
            Item[] items)
        {
            ClassName = className;
            RaceName = raceName;
            CovenantShortName = covenantShortName;
            HealthValue = healthValue;
            Position = position;
            Attributes = attributes;
            Items = items;
        }
    }
}
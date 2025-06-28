using System;
using System.Linq;
using Units;

namespace Services.SaveLoadSystem
{
    [Serializable]
    public class RunData
    {
        public int PathSeed;
        public string _path;
        public bool IsLastPathCellCompleted;
        public int DungeonID;
        public UnitData[] SomeUnitArray;
        
        public RunData(int dungeonID = -1, int pathSeed = 0)
        {
            PathSeed = pathSeed == 0? (int)DateTime.Now.Ticks & 0x0000FFFF : pathSeed;
            IsLastPathCellCompleted = false;
            DungeonID = dungeonID;
            _path = "";
        }

        public RunData()
        {
            PathSeed = 0;
            IsLastPathCellCompleted = false;
            DungeonID = -1;
            _path = "";
        }
        
        public void UpdatePath(int newIndex)
        {
            _path += $"{newIndex}/";
        }

        public int[] GetPath()
        {
            if (string.IsNullOrEmpty(_path))
                return Array.Empty<int>();

            return _path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }

        public void SavePlayerUnits(Unit[] units)
        {
            SomeUnitArray = units.Select(ConvertToUnitData).ToArray();
        }

        public Unit[] GetPlayerUnits()
        {
            return SomeUnitArray.Select(ConvertToUnit).ToArray();
        }

        private UnitData ConvertToUnitData(Unit unit)
        {
            var unitData = 
                new UnitData(unit.Class.Name, unit.Race.Name, unit.Covenant.ShortName, 0,
                    unit.Stats.CurrentHealth.Value, unit.Position, unit.Stats.Attributes, unit.ItemsHolder.Items.ToArray());
            return unitData;
        } 
        
        private Unit ConvertToUnit(UnitData unitData)
        {
            var unitClass = Class.GetObjectByName(unitData.ClassName);
            var unitRace = Race.GetObjectByName(unitData.RaceName);
            var unitCovenant = Covenant.GetObjectByName(unitData.CovenantShortName);
                
            var unit =
                new Unit(UnitPreset.GeneratePreset(unitClass, unitRace, unitCovenant, unitData.Attributes))
                    .WithHealth(unitData.HealthValue)
                    .WithPosition(unitData.Position)
                    .WithItems(unitData.Items);
            return unit;
        } 
    }
}
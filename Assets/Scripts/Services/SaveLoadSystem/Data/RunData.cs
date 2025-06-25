using System;
using System.Collections.Generic;
using System.Linq;
using Units;

namespace Services.SaveLoadSystem
{
    [Serializable]
    public class RunData
    {
        public int PathSeed;
        public string _path = "";
        public UnitData _unit1;
        public UnitData _unit2;
        public UnitData _unit3;
        public UnitData _unit4;
        public UnitData _unit5;
        public UnitData _unit6;
        public UnitData _unit7;
        public UnitData _unit8;
        public UnitData _unit9;

        public RunData(int pathSeed)
        {
            PathSeed = pathSeed;
            _path = "";
        }

        public RunData()
        {
            PathSeed = 0;
        }
        
        public void UpdatePath(int newIndex)
        {
            _path += $"/{newIndex}";
        }

        public int[] GetPath()
        {
            return _path.Split('/').Select(int.Parse).ToArray();
        }

        public void SavePlayerUnits(Unit[] units)
        {
            _unit1 = ConvertToUnitData(units[0]); 
            _unit2 = ConvertToUnitData(units[1]); 
            _unit3 = ConvertToUnitData(units[2]); 
            _unit4 = ConvertToUnitData(units[3]); 
            _unit5 = ConvertToUnitData(units[4]); 
            _unit6 = ConvertToUnitData(units[5]); 
            _unit7 = ConvertToUnitData(units[6]); 
            _unit8 = ConvertToUnitData(units[7]); 
            _unit9 = ConvertToUnitData(units[8]); 
        }

        public Unit[] GetPlayerUnits()
        {
            var units = new Unit[9];
            units[0] = ConvertToUnit(_unit1); 
            units[1] = ConvertToUnit(_unit2); 
            units[2] = ConvertToUnit(_unit3); 
            units[3] = ConvertToUnit(_unit4); 
            units[4] = ConvertToUnit(_unit5); 
            units[5] = ConvertToUnit(_unit6); 
            units[6] = ConvertToUnit(_unit7); 
            units[7] = ConvertToUnit(_unit8); 
            units[8] = ConvertToUnit(_unit9); 
            return units;
        }

        private UnitData ConvertToUnitData(Unit unit)
        {
            var unitData = 
                new UnitData(unit.Class.Name, unit.Race.Name, unit.Covenant.ShortName, 0,
                    unit.Stats.CurrentHealth.Value, unit.Position, unit.Stats.Attributes);
            return unitData;
        } 
        
        private Unit ConvertToUnit(UnitData unitData)
        {
            var unitClass = Class.GetObjectByName(unitData.ClassName);
            var unitRace = Race.GetObjectByName(unitData.RaceName);
            var unitCovenant = Covenant.GetObjectByName(unitData.CovenantShortName);
                
            var unit =
                new Unit(UnitPreset.GeneratePreset(unitClass, unitRace, unitCovenant, unitData.Attributes))
                    .WithHealth(unitData.HealthValue).WithPosition(unitData.Position);
            return unit;
        } 
    }
}
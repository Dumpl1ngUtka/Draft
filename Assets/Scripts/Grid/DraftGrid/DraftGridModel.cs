using System;
using System.Collections.Generic;
using System.Linq;
using Services.SaveLoadSystem;
using Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Grid.DraftGrid
{
    public class DraftGridModel : GridModel
    {
        private readonly List<Unit> _draftedUnits = new List<Unit>();
        
        public readonly ChemestryInteractor ChemestryInteractor = new();

        private RunData _runData;
        private List<Class> _availableClasses;
        private List<Race> _availableRaces;
        private List<Covenant> _availableCovenants;
        
        public int DraftedUnitsCount => _draftedUnits.Count;

        public DraftGridModel()
        {
            var profileData = SaveLoadService.Instance.LoadUnitsBelongingData();
            _runData = SaveLoadService.Instance.LoadRunData();
            _availableClasses = Class.GetObjectsByNames(profileData.AvailableUnitClasses).ToList();
            _availableRaces = Race.GetObjectsByNames(profileData.AvailableUnitRaces).ToList();
            _availableCovenants = Covenant.GetObjectsByNames(profileData.AvailableUnitCovenants).ToList();
        }
        
        public List<Unit> GetUnitsForDraft(int lineIndex, int count = 6)
        {
            var units = new List<Unit>();
            var availableClasses = _availableClasses.Where(unitClass => unitClass.LineIndexes.Contains(lineIndex)).ToList();
            
            for (int i = 0; i < count; i++)
            {
                var unitClass = availableClasses[Random.Range(0, availableClasses.Count)]; 
                var unitRace = _availableRaces[Random.Range(0, _availableRaces.Count)]; 
                var unitCovenant = _availableCovenants[Random.Range(0, _availableCovenants.Count)]; 
                var unit = new Unit(UnitPreset.GeneratePreset(unitClass, unitRace, unitCovenant));
                units.Add(unit);
            }
            
            return units;
        }

        public void AddUnit(Unit unit)
        {
            _draftedUnits.Add(unit);
            ChemestryInteractor.UnitAdded(unit);
        }
        
        public void DraftFinished()
        {
            SaveRunData();
            GameControlService.ChangeGrid(GameControlService.PathMapGridPrefab);
        }

        private void SaveRunData()
        {
            _runData.SavePlayerUnits(_draftedUnits.ToArray());
            SaveLoadService.Instance.SaveRunData(_runData);
        }
    }
}
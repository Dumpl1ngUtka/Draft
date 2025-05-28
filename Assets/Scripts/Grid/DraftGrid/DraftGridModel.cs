using System.Collections.Generic;
using System.Linq;
using Grid.Cells;
using Services.PanelService;
using Units;
using UnityEngine;

namespace Grid.DraftGrid
{
    public class DraftGridModel : GridModel
    {
        private readonly List<Unit> _draftedUnits = new List<Unit>();
        private List<Class> _availableClasses => GameControlService.CurrentDungeonInfo.Classes;
        
        public ChemestryInteractor ChemestryInteractor = new();
        public int DraftedUnitsCount => _draftedUnits.Count;

        public List<Unit> GetUnitsForDraft(int lineIndex, int count = 6)
        {
            var units = new List<Unit>();
            
            var availableClasses = 
                _availableClasses.Where(x => x.LineIndexes.Contains(lineIndex)).ToList();
            for (int i = 0; i < count; i++)
            {
                var unit = UnitPreset.GenerateUnit(availableClasses[Random.Range(0, availableClasses.Count)]);
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
            GameControlService.CurrentRunInfo.SavePlayerUnits(_draftedUnits);
            GameControlService.ChangeGrid(GameControlService.BattleGridPrefab);
        }
    }
}
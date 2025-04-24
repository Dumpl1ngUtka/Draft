using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.Units;
using Grid.Cells;
using Services.GameControlService;
using Services.PanelService;
using UnityEngine;

namespace Grid.DraftGrid
{
    public class DraftGridModel : GridModel
    {
        private List<Class> _availableClasses => GameControlService.Instance.Classes;
        private List<Race> _availableRaces => GameControlService.Instance.Races;
        private List<UnitGridCell> _cells;
        
        public DraftGridModel(List<UnitGridCell> cells)
        {
            _cells = cells;
        }
        
        public void TryFinish()
        {
            if (IsAllCellsFilled())
                GameControlService.Instance.FinishDraftLevel(_cells.Select(cell => cell.Unit).ToList());
            else
                PanelService.Instance.InstantiateErrorPanel("draft_fill_cells_error");
        }
        
        private bool IsAllCellsFilled()
        {
            var fillCellsCount = _cells.Count(x => x.Unit != null);
            return fillCellsCount == _cells.Count;
        }

        public List<Unit> GetUnitsForDraft(UnitGridCell generateForCell, int count = 6)
        {
            var units = new List<Unit>();
            
            var availableClasses = 
                _availableClasses.Where(x => x.LineIndexes.Contains(generateForCell.LineIndex)).ToList();
            for (int i = 0; i < count; i++)
            {
                var preset = UnitPreset.Generate(availableClasses[Random.Range(0, availableClasses.Count)]);
                units.Add(new Unit(preset));
            }
            
            return units;
        }

        public void SetUnitToCell(Unit unit, UnitGridCell unitGridCell)
        {
            unitGridCell.AddUnit(unit);
        }
        
        public List<UnitGridCell> GetSwichCells(UnitGridCell from)
        {
            var cells = _cells.Where(cell => from.Unit.Class.LineIndexes.Contains(cell.LineIndex)).ToList();
            return cells.Where(cell => (cell.Unit != null && cell.Unit.Class.LineIndexes.Contains(from.LineIndex)) || cell.Unit == null).ToList();
        }
        
        public List<UnitGridCell> GetNoSwichCells(UnitGridCell from)
        {
            var cells = _cells.Where(cell => !from.Unit.Class.LineIndexes.Contains(cell.LineIndex)).ToList();
            return cells.Where(cell => (cell.Unit != null && cell.Unit.Class.LineIndexes.Contains(from.LineIndex)) || cell.Unit == null).ToList();
        }

        public void SwitchCard(UnitGridCell from, UnitGridCell to)
        {
            if (!GetSwichCells(from).Contains(to))
            {
                PanelService.Instance.InstantiateErrorPanel("line_index_mismatch_error");
                return;
            }
            
            if (from.TeamType != to.TeamType)
            {
                PanelService.Instance.InstantiateErrorPanel("team_index_mismatch_error");
                return;
            }
            
            var toUnit = to.Unit;
            to.AddUnit(from.Unit);
            from.RemoveUnit();
            if (toUnit != null)
                from.AddUnit(toUnit);
        }

        public void QuickFinish()
        {
            foreach (var cell in _cells)
            {
                var unit = new Unit(UnitPreset.Generate(_availableClasses[Random.Range(0, _availableClasses.Count)]));
                cell.AddUnit(unit);
            }
            TryFinish();
        }
    }
}
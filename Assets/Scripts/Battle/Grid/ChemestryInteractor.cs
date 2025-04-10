using System.Collections.Generic;
using System.Linq;
using Battle.Units;

namespace Battle.Grid
{
    public class ChemestryInteractor
    {
        private readonly Dictionary<Race, int> _raceCounts = new Dictionary<Race, int>();
        private readonly Dictionary<Covenant, int> _covenantCounts = new Dictionary<Covenant, int>();
        private readonly Dictionary<CovenantType, int> _covenantTypeCounts = new Dictionary<CovenantType, int>();
        private readonly List<GridCell> _cells;
        
        public int AllTeamChem {get; private set;}

        public ChemestryInteractor(List<GridCell> cells)
        {
            _cells = cells;
        }

        public void UnitAdded(Unit unit)
        {
            UpdateCounters(unit);
            AddChemistryToUnits();
        }
        
        private void UpdateCounters(Unit unit)
        {
            if (!_raceCounts.TryAdd(unit.Race, 1))
                _raceCounts[unit.Race]++;

            if (!_covenantCounts.TryAdd(unit.Covenant, 1))
                _covenantCounts[unit.Covenant]++;

            if (!_covenantTypeCounts.TryAdd(unit.Covenant.Type, 1))
                _covenantTypeCounts[unit.Covenant.Type]++;
        }
        
        private void AddChemistryToUnits()
        {
            AllTeamChem = 0;
            foreach (var cell in _cells)
            {
                var unit = cell.Unit;
                var totalChem = 0;
                
                if (unit == null)
                    continue;
                
                if (_raceCounts.TryGetValue(unit.Race, out var raceVal))
                    totalChem += raceVal - 1;
                
                if (_covenantCounts.TryGetValue(unit.Covenant, out var covenantVal))
                    totalChem += covenantVal - 1;

                if (_covenantTypeCounts.TryGetValue(unit.Covenant.Type, out var covenantTypeVal))
                    totalChem += covenantTypeVal - 1;
                
                unit.SetChemistry(totalChem);
                AllTeamChem += totalChem;
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using CustomObserver;
using Grid.Cells;
using Units;

namespace Grid.DraftGrid
{
    public class ChemestryInteractor : IObservable<ChemestryInteractor>
    {
        public int AllTeamChem {get; private set;}
        public readonly Dictionary<Race, int> RaceCounts = new Dictionary<Race, int>();
        public readonly Dictionary<Covenant, int> CovenantCounts = new Dictionary<Covenant, int>();
        public readonly Dictionary<CovenantType, int> CovenantTypeCounts = new Dictionary<CovenantType, int>();

        private List<UnitGridCell> _cells;
        private readonly List<IObserver<ChemestryInteractor>> _observers = new();


        public ChemestryInteractor(List<UnitGridCell> cells)
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
            if (!RaceCounts.TryAdd(unit.Race, 1))
                RaceCounts[unit.Race]++;

            if (!CovenantCounts.TryAdd(unit.Covenant, 1))
                CovenantCounts[unit.Covenant]++;

            if (!CovenantTypeCounts.TryAdd(unit.Covenant.Type, 1))
                CovenantTypeCounts[unit.Covenant.Type]++;
        }
        
        private void AddChemistryToUnits()
        {
            AllTeamChem = 0;
            var units = _cells.Select(cell => cell.Unit);
            foreach (var unit in units)
            {
                var totalChem = 0;
                
                if (unit == null)
                    continue;
                
                if (RaceCounts.TryGetValue(unit.Race, out var raceVal))
                    totalChem += raceVal - 1;
                
                if (CovenantCounts.TryGetValue(unit.Covenant, out var covenantVal))
                    totalChem += covenantVal - 1;

                if (CovenantTypeCounts.TryGetValue(unit.Covenant.Type, out var covenantTypeVal))
                    totalChem += covenantTypeVal - 1;

                var unitChem = unit.Stats.Chemistry;
                unitChem.AddModifier(new PermanentStatModifier(totalChem - unitChem.Value));
                AllTeamChem += totalChem;
            }

            NotifyObservers();
        }

        public void AddObserver(IObserver<ChemestryInteractor> observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver<ChemestryInteractor> observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
                observer.UpdateObserver(this);
        }
    }
}
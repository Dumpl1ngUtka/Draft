using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using UnityEngine;

namespace Battle.Grid
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private int _dungeonLevel;
        [SerializeField] private Class[] _availableClasses;
        [SerializeField] private Race[] _availableRaces;
        [SerializeField] private GridScheme _gridScheme;
        [SerializeField] private GridFiller _gridFiller;
        [SerializeField] private GridRenderer _gridRenderer;
        [SerializeField] private SelectUnitsMenu.SelectUnitsMenu _selectUnitsMenu;
        private GridCell[] _cells;
        private readonly Dictionary<Race, int> _raceCounts = new Dictionary<Race, int>();
        private readonly Dictionary<Covenant, int> _covenantCounts = new Dictionary<Covenant, int>();
        private readonly Dictionary<CovenantType, int> _covenantTypeCounts = new Dictionary<CovenantType, int>();
        
        public int AllTeamChem {get; private set;}
        public  int AllTeamSumLevel {get; private set;} = 0;
        public  int FillCellsCount {get; private set;} = 0;
        
        public Action TeamChanged;
        
        private void Awake()
        {
            _gridRenderer.Init(this);
            _cells = _gridFiller.Fill(this, _gridScheme);
        }

        public void ShowUnitSelectMenu(int cellID)
        {
            _selectUnitsMenu.Init(this, cellID);
            var availableClasses = _availableClasses.Where(x => x.LineIndex == _cells[cellID].LineIndex).ToArray();
            _selectUnitsMenu.Enable();
            CalculateLevel(out int midLevel, out int delta);
            _selectUnitsMenu.GenerateCards(availableClasses, midLevel, delta);
        }

        private void CalculateLevel(out int midLevel, out int delta)
        {
            if (FillCellsCount < _cells.Length / 2)
            {
                midLevel = _dungeonLevel/2;
                delta =  midLevel / 2;
            }
            else
            {
                var averageLevel = AllTeamSumLevel / FillCellsCount;
                
                midLevel = averageLevel < _dungeonLevel / 8 * 6? _dungeonLevel / 8 * 7 : _dungeonLevel / 8 * 5;
                delta = _dungeonLevel / 8;
            }

        }

        public void AddCard(int cellID, Unit unit)
        {
            _cells[cellID].AddUnit(unit);
            CalculateChemistry(unit);
            AddChemistryToUnits();
            FillCellsCount++;
            AllTeamSumLevel += unit.Level; 
            TeamChanged?.Invoke();
        }

        private void CalculateChemistry(Unit newUnit)
        {
            if (!_raceCounts.TryAdd(newUnit.Race, 1))
                _raceCounts[newUnit.Race]++;

            if (!_covenantCounts.TryAdd(newUnit.Covenant, 1))
                _covenantCounts[newUnit.Covenant]++;

            if (!_covenantTypeCounts.TryAdd(newUnit.Covenant.Type, 1))
                _covenantTypeCounts[newUnit.Covenant.Type]++;
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
                
                unit.SetChemestry(totalChem);
                AllTeamChem += totalChem;
            }
        }
    }
}

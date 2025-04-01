using System;
using System.Collections.Generic;
using System.Linq;
using Battle.InfoPanel;
using Battle.Units;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Grid
{
    public class DraftGrid : Grid
    {
        [Header("Draft grid settings")]
        [SerializeField] private int _dungeonLevel;
        [SerializeField] private Class[] _availableClasses;
        [SerializeField] private Race[] _availableRaces;
        [SerializeField] private GridRenderer _gridRenderer;
        [SerializeField] private SelectUnitsMenu.SelectUnitsMenu _selectUnitsMenu;
        [SerializeField] private CardInfoPanel _cardInfoPrefab;
        [SerializeField] private TMP_Text _totalChemText;
        [SerializeField] private TMP_Text _avaregeLevelText;
        private readonly Dictionary<Race, int> _raceCounts = new Dictionary<Race, int>();
        private readonly Dictionary<Covenant, int> _covenantCounts = new Dictionary<Covenant, int>();
        private readonly Dictionary<CovenantType, int> _covenantTypeCounts = new Dictionary<CovenantType, int>();

        public int AllTeamChem {get; private set;}
        public int AllTeamSumLevel {get; private set;} = 0;
        public int FillCellsCount {get; private set;} = 0;
        
        public Action TeamChanged;

        private void Render()
        {
            _totalChemText.text = AllTeamChem.ToString();
            _avaregeLevelText.text = (AllTeamSumLevel / FillCellsCount).ToString();
        }
        
        protected override void Clicked(GridCell cell)
        {
            if (cell.Unit == null)
                ShowUnitSelectMenu(cell.Index);
            else 
                ShowCardInfo(cell.Unit);
        }

        private void ShowCardInfo(Unit cellUnit)
        {
            _cardInfoPrefab.Instantiate(cellUnit);
            _cardInfoPrefab.Render(cellUnit);
        }

        protected override void Dragged(GridCell from, GridCell to)
        {
            SwitchCard(from, to);
        }

        private void ShowUnitSelectMenu(int cellID)
        {
            _selectUnitsMenu.Init(this, cellID);
            var availableClasses = _availableClasses.Where(x => x.LineIndex == Cells[cellID].LineIndex).ToArray();
            _selectUnitsMenu.Enable();
            CalculateLevel(out int midLevel, out int delta);
            _selectUnitsMenu.GenerateCards(availableClasses, midLevel, delta);
        }

        private void CalculateLevel(out int midLevel, out int delta)
        {
            if (FillCellsCount < Cells.Length / 2)
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
            Cells[cellID].AddUnit(unit);
            CalculateChemistryFor(unit);
            AddChemistryToUnits();
            FillCellsCount++;
            AllTeamSumLevel += unit.Level; 
            TeamChanged?.Invoke();
            Render();
        }

        private void SwitchCard(GridCell from, GridCell to)
        {
            if (from.Unit == null)
                return;

            if (from.LineIndex != to.LineIndex)
                return;
            
            var toUnit = to.Unit;
            to.AddUnit(from.Unit);
            from.RemoveUnit();
            if (toUnit != null)
                from.AddUnit(toUnit);
        }

        private void CalculateChemistryFor(Unit unit)
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
            foreach (var cell in Cells)
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

        public List<Unit> GetUnits()
        {
            return Cells.Select(cell => cell.Unit).ToList();
        } 
    }
}
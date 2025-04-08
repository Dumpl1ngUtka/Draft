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
        [SerializeField] private Transform _container;
        private readonly Dictionary<Race, int> _raceCounts = new Dictionary<Race, int>();
        private readonly Dictionary<Covenant, int> _covenantCounts = new Dictionary<Covenant, int>();
        private readonly Dictionary<CovenantType, int> _covenantTypeCounts = new Dictionary<CovenantType, int>();
        private List<GridCell> _cells;
        private bool _isDragBeginSuccess;
        
        public int AllTeamChem {get; private set;}
        public int AllTeamSumLevel {get; private set;} = 0;
        public int FillCellsCount {get; private set;} = 0;
        
        public Action DraftFinished;


        public override void Init()
        {
            _cells = InitiateCells(LineCount, ColumnCount, PlayerTeamID, _container);
        }
        
        private void Render()
        {
            _totalChemText.text = AllTeamChem.ToString();
            _avaregeLevelText.text = (AllTeamSumLevel / FillCellsCount).ToString();
        }

        protected override void DragOverCell(GridCell from, GridCell over)
        {
        }

        protected override void DragBegin(GridCell from)
        {
            _isDragBeginSuccess = false;
            if (from.Unit == null)
            {
                InstantiateErrorPanel("unit_null_error");
                return;
            }

            foreach (var cell in GetSwichCells(from))
            {
                cell.Renderer.SetSize(1.1f);
            }
            _isDragBeginSuccess = true;
        }

        private List<GridCell> GetSwichCells(GridCell from)
        {
            var cells = _cells.Where(cell => from.Unit.Class.LineIndexes.Contains(cell.LineIndex)).ToList();
            return cells.Where(cell => (cell.Unit != null && cell.Unit.Class.LineIndexes.Contains(from.LineIndex)) || cell.Unit == null).ToList();

        }

        protected override void Clicked(GridCell cell)
        {
            if (cell.Unit == null)
                ShowUnitSelectMenu(cell);
            else 
                ShowCardInfo(cell.Unit);
        }

        private void ShowCardInfo(Unit cellUnit)
        {
            _cardInfoPrefab.Instantiate(cellUnit);
            _cardInfoPrefab.Render(cellUnit);
        }

        protected override void DragFinished(GridCell from, GridCell to)
        {
            if (!_isDragBeginSuccess)
                return;
            
            foreach (var cell in GetSwichCells(from))
            {
                cell.Renderer.SetSize(1f);
            }
            
            SwitchCard(from, to);
        }

        private void ShowUnitSelectMenu(GridCell cell)
        {
            _selectUnitsMenu.Init(this, cell);
            var availableClasses = _availableClasses.Where(x => x.LineIndexes.Contains(cell.LineIndex)).ToArray();
            _selectUnitsMenu.Enable();
            _selectUnitsMenu.GenerateCards(availableClasses);
        }
        

        public void AddCard(GridCell cell, Unit unit)
        {
            cell.AddUnit(unit);
            CalculateChemistryFor(unit);
            AddChemistryToUnits();
            FillCellsCount++;
            Render();
        }

        private void SwitchCard(GridCell from, GridCell to)
        {
            if (!GetSwichCells(from).Contains(to))
            {
                InstantiateErrorPanel("line_index_mismatch_error");
                return;
            }
            
            if (from.TeamIndex != to.TeamIndex)
            {
                InstantiateErrorPanel("team_index_mismatch_error");
                return;
            }
            
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

        public List<Unit> GetUnits()
        {
            return _cells.Select(cell => cell.Unit).ToList();
        }

        public void PressFinishButton()
        {
            if (FillCellsCount < 9)
            {
                InstantiateErrorPanel("draft_fill_cells_error");
                return;
            }
            
            DraftFinished?.Invoke();
        }
    }
}
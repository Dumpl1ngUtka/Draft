using System.Collections.Generic;
using System.Linq;
using DungeonMap;
using Grid.Cells;
using Services.SaveLoadSystem;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Grid.BattleGrid
{
    public class BattleGridView : GridView
    {
        [SerializeField] private Image _background; 
        private List<UnitGridCell> _playerCells;
        private List<UnitGridCell> _enemyCells;

        public List<UnitGridCell> GetCells()
        {
            GetCells(out var playerCells, out var enemyCells);
            return playerCells.Concat(enemyCells).ToList();
        }

        public void InitBackgroundTexture()
        {
            var dungeonID = SaveLoadService.Instance.LoadRunData().DungeonID;
            _background.sprite = DungeonInfo.GetObjectByID(dungeonID).BackgroundTexture;
        }
        
        public void GetCells(out List<UnitGridCell> playerCells, out List<UnitGridCell> enemyCells)
        {
            playerCells = _playerCells ?? GetUnitsCellsByTeam(TeamType.Player);
            enemyCells = _enemyCells ?? GetUnitsCellsByTeam(TeamType.Enemy);
        }

        public void SetSizeFor(float size, List<Unit> units)
        {
            var cells = GetCellsFromUnits(units);
            foreach (var cell in cells) 
                cell.Renderer.SetSize(size);
        }

        public void ResetSize()
        {
            foreach (var cell in GetCells()) 
                cell.Renderer.SetSize(1f);
        }

        public void SetSpriteColorFor(Unit unit, Color color)
        {
            GetCellFromUnit(unit).Renderer.SetSpriteColor(color);
        }
        
        public void SetSpriteColorFor(List<Unit> units, Color color)
        {
            foreach (var cell in GetCellsFromUnits(units)) 
                cell.Renderer.SetSpriteColor(color);
        }

        public void SetDiceAdditionValue(List<Unit> cells, int value)
        {
            foreach (var cell in GetCellsFromUnits(cells)) 
                cell.Renderer.RenderDiceAdditionValue(value);
        }
        
        public void SetDiceAdditionValue(Unit unit, int value)
        {
            GetCellFromUnit(unit).Renderer.RenderDiceAdditionValue(value);
        }
        
        public void ResetSpriteColor()
        {
            foreach (var cell in GetCells()) 
                cell.Renderer.SetSpriteColor(Color.white);
        }

        public void ResetDiceAdditionValue()
        {
            foreach (var unit in GetCells().Select(x => x.Unit))
                SetDiceAdditionValue(unit, 0);
        }
        
        private List<UnitGridCell> GetCellsFromUnits(List<Unit> units)
        {
            return GetCells().Where(cell => units.Contains(cell.Unit)).ToList();
        }
        
        private UnitGridCell GetCellFromUnit(Unit unit)
        {
            return GetCells().FirstOrDefault(cell => cell.Unit == unit);
        }
    }
}
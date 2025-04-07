using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.Units.Interactors;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.UseCardReactions
{
    [CreateAssetMenu(menuName = "Config/Reactions/ChangeRowOrColumn")]
    public class ChangeRowOrColumn : Reaction
    {
        [Header("Value")]
        [SerializeField] private int _value = 1;
        [Header("Line")]
        [SerializeField] private bool _islineUpgradable;
        [SerializeField] private bool _isUnitLineUpgradable;
        [SerializeField, Range(0, 2)] private int _line;
        [Header("Column")]
        [SerializeField] private bool _isColumnUpgradable;
        [SerializeField] private bool _isUnitColumnUpgradable;
        [SerializeField, Range(0, 2)] private int _column;
        
        public override Response TryUseReaction(GridCell casterCell, List<GridCell> cells)
        {
            var upgradableCells = new List<GridCell>();
            if (_islineUpgradable)
                upgradableCells.AddRange(GetUpgradeLine(casterCell, cells));
            if (_isColumnUpgradable)
                upgradableCells.AddRange(GetUpgradeColumn(casterCell, cells));
            upgradableCells = upgradableCells.Distinct().ToList();
            foreach (var cell in upgradableCells)
            {
                cell.Unit.SetPower(cell.Unit.DicePower + _value);
            }
            return new Response(true, "ReactionUsed");
        }

        private List<GridCell> GetUpgradeLine(GridCell casterCell, List<GridCell> cells)
        {
            var upgradableLineIndex = _isUnitLineUpgradable?  casterCell.LineIndex : _line;
            return cells.Where(x => x.LineIndex == upgradableLineIndex).ToList();
        }
        
        private List<GridCell> GetUpgradeColumn(GridCell casterCell, List<GridCell> cells)
        {
            var upgradableLineIndex = _isUnitColumnUpgradable?  casterCell.ColumnIndex : _column;
            return cells.Where(x => x.ColumnIndex == upgradableLineIndex).ToList();
        }
    }
}
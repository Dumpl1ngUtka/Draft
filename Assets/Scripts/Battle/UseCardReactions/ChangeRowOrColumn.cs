using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.Units.Interactors;
using Grid.Cells;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.UseCardReactions
{
    [CreateAssetMenu(menuName = "Config/Reactions/ChangeRowOrColumn")]
    public class ChangeRowOrColumn : Reaction
    {
        [Header("Line")]
        [SerializeField] private bool _islineUpgradable;
        [SerializeField] private bool _isUnitLineUpgradable;
        [SerializeField, Range(0, 2)] private int _line;
        [Header("Column")]
        [SerializeField] private bool _isColumnUpgradable;
        [SerializeField] private bool _isUnitColumnUpgradable;
        [SerializeField, Range(0, 2)] private int _column;

        public override List<UnitGridCell> GetReactionCells(UnitGridCell caster, List<UnitGridCell> allies)
        {
            var upgradableCells = new List<UnitGridCell>();
            if (_islineUpgradable)
                upgradableCells.AddRange(GetUpgradeLine(caster, allies));
            if (_isColumnUpgradable)
                upgradableCells.AddRange(GetUpgradeColumn(caster, allies));
            return upgradableCells.Distinct().ToList();
        }

        private List<UnitGridCell> GetUpgradeLine(UnitGridCell casterCell, List<UnitGridCell> cells)
        {
            var upgradableLineIndex = _isUnitLineUpgradable?  casterCell.LineIndex : _line;
            return cells.Where(x => x.LineIndex == upgradableLineIndex).ToList();
        }
        
        private List<UnitGridCell> GetUpgradeColumn(UnitGridCell casterCell, List<UnitGridCell> cells)
        {
            var upgradableLineIndex = _isUnitColumnUpgradable?  casterCell.ColumnIndex : _column;
            return cells.Where(x => x.ColumnIndex == upgradableLineIndex).ToList();
        }
    }
}
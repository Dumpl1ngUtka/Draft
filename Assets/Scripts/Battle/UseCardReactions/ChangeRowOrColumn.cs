using System.Collections.Generic;
using System.Linq;
using Grid.Cells;
using Units;
using UnityEngine;

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

        public override List<Unit> GetReactionCells(Unit caster, List<Unit> allies)
        {
            var upgradableCells = new List<Unit>();
            if (_islineUpgradable)
                upgradableCells.AddRange(GetUpgradeLine(caster, allies));
            if (_isColumnUpgradable)
                upgradableCells.AddRange(GetUpgradeColumn(caster, allies));
            return upgradableCells.Distinct().ToList();
        }

        private List<Unit> GetUpgradeLine(Unit casterCell, List<Unit> cells)
        {
            var upgradableLineIndex = _isUnitLineUpgradable?  casterCell.Position.LineIndex : _line;
            return cells.Where(x => x.Position.LineIndex == upgradableLineIndex).ToList();
        }
        
        private List<Unit> GetUpgradeColumn(Unit casterCell, List<Unit> cells)
        {
            var upgradableLineIndex = _isUnitColumnUpgradable?  casterCell.Position.ColumnIndex : _column;
            return cells.Where(x => x.Position.ColumnIndex == upgradableLineIndex).ToList();
        }
    }
}
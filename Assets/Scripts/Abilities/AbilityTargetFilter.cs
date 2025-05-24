using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Grid.Cells;
using UnityEngine;

namespace Battle.Abilities
{
    [Serializable]
    public class AbilityTargetFilter
    {
        public bool IsPlayerUnitTarget;
        public bool IsEnemyUnitTarget;
        public bool IsDeadPlayerUnitTarget;
        public bool IsDeadEnemyUnitTarget;

        public bool IsRightTarget(UnitGridCell caster, UnitGridCell target)
        {
            var isTeammete = target.TeamType == caster.TeamType;
            var isDead = target.Unit.Stats.IsDead;
            
            if (IsPlayerUnitTarget && isTeammete && !isDead)
                return true;
            if (IsEnemyUnitTarget && !isTeammete && !isDead)
                return true;
            if (IsDeadPlayerUnitTarget && isTeammete && isDead)
                return true;
            if (IsDeadEnemyUnitTarget && !isTeammete && isDead)
                return true;

            return false;
        }
    }
}
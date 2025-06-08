using System;
using Grid.Cells;
using Units;

namespace Abilities
{
    [Serializable]
    public class AbilityTargetFilter
    {
        public bool IsSelfTarget;
        public bool IsPlayerUnitTarget;
        public bool IsEnemyUnitTarget;
        public bool IsDeadPlayerUnitTarget;
        public bool IsDeadEnemyUnitTarget;

        public bool IsRightTarget(Unit caster, Unit target)
        {
            var isSelfCast = caster == target;
            var isTeammete = target.Position.TeamType == caster.Position.TeamType;
            var isDead = target.Stats.IsDead;

            if (IsSelfTarget && isSelfCast)
                return true;
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
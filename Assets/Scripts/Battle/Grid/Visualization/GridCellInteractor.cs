using System;
using Battle.Units;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    public abstract class GridCellInteractor 
    {
        [SerializeField] private CellPreset _preset;
        private bool _isActive;

        public void SetActive(CellPresetType activatedPresetType)
        {
            if (activatedPresetType == CellPresetType.None)
                _isActive = false;
            else if (activatedPresetType == CellPresetType.All)
                _isActive = true;
            else
                _isActive = _preset.Include(activatedPresetType);
            ActiveChanged(_isActive);
        }

        public void Render(Unit unit)
        {
            if (!_isActive)
                return;
            UpdateInfo(unit);
        }
        
        protected abstract void ActiveChanged(bool isActive);
        
        protected abstract void UpdateInfo(Unit unit);
    }
}
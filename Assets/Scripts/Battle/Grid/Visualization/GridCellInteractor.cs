using Units;

namespace Battle.Grid.Visualization
{
    public abstract class GridCellInteractor 
    {
        protected Unit Unit;
        protected UnitGridCellRenderer Renderer;
        
        public void Init(Unit unit, UnitGridCellRenderer renderer)
        {
            Unit = unit;
            Renderer = renderer;
            SetActive(unit != null);
        }

        public void TryUpdateInfo()
        {
            if (Unit != null)
                UpdateInfo();
        }
        
        protected abstract void UpdateInfo();
        
        protected abstract void SetActive(bool isActive); 
    }
}
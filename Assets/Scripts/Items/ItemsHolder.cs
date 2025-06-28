using System.Collections.Generic;
using Battle.PassiveEffects;
using Services.PanelService;
using Units;

namespace Items
{
    public class ItemsHolder
    {
        private readonly PassiveEffectsHolder _effectsHolder;
        private readonly UnitStats _unitStats;
        public int Capacity { get; private set; }
        public List<Item> Items { get; }
        public int Weight {get; private set;} 

        public ItemsHolder(UnitStats unitStats, PassiveEffectsHolder effectsHolder)
        {
            Items = new List<Item>();
            Weight = 0;
            Capacity = unitStats.Capacity.Value;
            _effectsHolder = effectsHolder;
            _unitStats = unitStats;
        }
        
        public bool AddItem(Item item)
        {
            if (Weight + item.Weight > Capacity)
            {
                PanelService.Instance.InstantiateErrorPanel("capacity_error");
                return false;
            }
            Weight += item.Weight;
            Items.Add(item);
            _effectsHolder.AddEffect(item.ItemEffect.GetInstance(_unitStats, _unitStats, 1000));
            return true;
        }

        public void AddItems(Item[] items)
        {
            foreach (var item in items)
            {
                if (!AddItem(item))
                    break;
            }
        }

        public void RemoveItem(Item item)
        {
            Weight -= item.Weight;
            Items.Remove(item);
        }
    }
}
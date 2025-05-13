using System;
using DungeonMap;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services.PanelService.Panels
{
    public class DungeonInfoPanel : InfoPanel
    {
        [SerializeField] private RectTransform _cardInfoPanel;
        [SerializeField] private TMP_Text _dungeonName;
        [SerializeField] private TMP_Text _dungeonDescription;
        private Action<DungeonInfo> _callback;
        private DungeonInfo _info;
        private float _maxRotation = 3;
        
        public void Init(DungeonInfo dungeon, Action<DungeonInfo> callback)
        {
            _info = dungeon;
            _callback = callback;
            Render(dungeon);
            SetRandomRotation();
        }

        private void Render(DungeonInfo dungeon)
        {
            _dungeonName.text = dungeon.Name;
            _dungeonDescription.text = dungeon.Description;
            
        }
        
        private void SetRandomRotation()
        {
            _cardInfoPanel.rotation = Quaternion.Euler(0, 0, Random.Range(-_maxRotation, _maxRotation));
        }

        public void OnApplyButtonClicked()
        {
            _callback?.Invoke(_info);
            Destroy();
        }
    }
}
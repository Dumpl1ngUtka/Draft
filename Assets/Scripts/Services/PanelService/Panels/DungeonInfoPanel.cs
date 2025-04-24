using System;
using DungeonMap;
using TMPro;
using UnityEngine;

namespace Services.PanelService.Panels
{
    public class DungeonInfoPanel : InfoPanel
    {
        [SerializeField] private TMP_Text _dungeonName;
        [SerializeField] private TMP_Text _dungeonDescription;
        private Action<DungeonInfo> _callback;
        private DungeonInfo _info;
        
        public void Init(DungeonInfo dungeon, Action<DungeonInfo> callback)
        {
            _info = dungeon;
            _callback = callback;
        }

        public void OnApplyButtonClicked() => _callback?.Invoke(_info);
    }
}
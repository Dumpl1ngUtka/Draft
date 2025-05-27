using System;
using DungeonMap;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Services.PanelService.Panels
{
    public class DungeonInfoPanel : Panel
    {
        [SerializeField] private Image _dungeonImage;
        [SerializeField] private TMP_Text _dungeonName;
        [SerializeField] private TMP_Text _dungeonDescription;
        private Action<DungeonInfo> _callback;
        private DungeonInfo _info;
        
        public void Init(DungeonInfo dungeon, Action<DungeonInfo> callback)
        {
            _info = dungeon;
            _callback = callback;
            Render(dungeon);
            SetRandomRotation();
        }

        private void Render(DungeonInfo dungeon)
        {
            _dungeonImage.sprite = dungeon.Image;
            _dungeonName.text = dungeon.Name;
            _dungeonDescription.text = dungeon.Description;
        }
        
        public void OnApplyButtonClicked()
        {
            _callback?.Invoke(_info);
            Destroy();
        }
    }
}
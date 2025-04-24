using System;
using Battle.Units;
using DungeonMap;
using Services.PanelService.Panels;
using UnityEngine;

namespace Services.PanelService
{
    public class PanelService : MonoBehaviour
    {
        private InfoPanel _infoPanelPrefab;
        private ErrorPanel _errorPanelPrefab;
        private CardInfoPanel _cardInfoPanelPrefab;
        private DungeonInfoPanel _dungeonInfoPanelPrefab;
        private Canvas _canvas;

        public Canvas Canvas
        {
            get
            {
                if (_canvas == null)
                    _canvas = FindAnyObjectByType<Canvas>();
                return _canvas;
            }
        }
        public static PanelService Instance { get; private set; }
        
        public void Init()
        {
            Instance = FindAnyObjectByType<PanelService>();
            _infoPanelPrefab = Resources.Load<InfoPanel>($"Prefabs/Panels/InfoPanel");
            _errorPanelPrefab = Resources.Load<ErrorPanel>($"Prefabs/Panels/ErrorPanel");
            _cardInfoPanelPrefab = Resources.Load<CardInfoPanel>($"Prefabs/Panels/CardInfoPanel");
            _dungeonInfoPanelPrefab = Resources.Load<DungeonInfoPanel>($"Prefabs/Panels/DungeonInfoPanel");
        }
        
        public void InstantiateUnitInfoPanel(Unit unit)
        {
            var panel = Instantiate(_cardInfoPanelPrefab, Canvas.transform);
            panel.Init(unit);
        }

        public void InstantiateDungeonInfoPanel(DungeonInfo dungeon, Action<DungeonInfo> callback)
        {
            var panel = Instantiate(_dungeonInfoPanelPrefab, Canvas.transform);
            panel.Init(dungeon, callback);
        }

        public void InstantiateErrorPanel(string keyWord)
        {
            var panel = Instantiate(_errorPanelPrefab, Canvas.transform);
            panel.Init(keyWord);
        }
        
        public void InstantiateInfoPanel(string keyWord)
        {
            var panel = Instantiate(_infoPanelPrefab, Canvas.transform);
            panel.Init(keyWord);
        }
    }
}
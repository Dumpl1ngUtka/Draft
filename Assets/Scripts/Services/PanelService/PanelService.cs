using Battle.Units;
using Services.PanelService.Panels;
using UnityEngine;

namespace Services.PanelService
{
    public class PanelService : MonoBehaviour
    {
        private Panels.InfoPanel _infoPanelPrefab;
        private ErrorPanel _errorPanelPrefab;
        private CardInfoPanel _cardInfoPanelPrefab;
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
            _infoPanelPrefab = Resources.Load<Panels.InfoPanel>($"Prefabs/Panels/InfoPanel");
            _errorPanelPrefab = Resources.Load<ErrorPanel>($"Prefabs/Panels/ErrorPanel");
            _cardInfoPanelPrefab = Resources.Load<CardInfoPanel>($"Prefabs/Panels/CardInfoPanel");
        }
        
        public void InstantiateCardInfoPanel(Unit unit)
        {
            var panel = Instantiate(_cardInfoPanelPrefab, Canvas.transform);
            panel.Init(unit);
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
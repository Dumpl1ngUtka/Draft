using System;
using Battle.Grid;
using Services.GameControlService;
using Services.PanelService;
using UnityEngine;

namespace Bootstrappers
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private DraftGrid _draftGrid;
        [SerializeField] private BattleGrid _battleGrid;
        [SerializeField] private Canvas _canvas;
        
        private void Awake()
        {
            CreatePanelSevice();
            CreateGameControlService();
        }

        private void CreatePanelSevice()
        {
            var obj = new GameObject("PanelSevice");
            var service = obj.AddComponent<PanelService>();
            service.Init();
            DontDestroyOnLoad(obj);
        }
        
        private void CreateGameControlService()
        {
            var obj = new GameObject("GameControlService");
            var service = obj.AddComponent<GameControlService>();
            service.Init(_draftGrid, _battleGrid);
            DontDestroyOnLoad(obj);
        }
        
        /*private void CreateSaveLoadService()
        {
            var jsonSaveLoadService = new JsonSaveLoadRepository();
            var obj = new GameObject("SaveLoadService");
            var service = obj.AddComponent<SaveLoadService>();
            service.Init(jsonSaveLoadService);
            DontDestroyOnLoad(obj);
        }*/

    }
}
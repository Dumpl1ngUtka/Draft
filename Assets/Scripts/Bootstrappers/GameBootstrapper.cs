using Grid.BattleGrid;
using Grid.DraftGrid;
using Grid.PathMapGrid;
using Grid.SelectDungeonGrid;
using Services.GameControlService;
using Services.GlobalAnimationService;
using Services.PanelService;
using UnityEngine;

namespace Bootstrappers
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private DraftGridController _draftGrid;
        [SerializeField] private BattleGridController _battleGrid;
        [SerializeField] private SelectDungeonGridController _dungeonGrid;
        [SerializeField] private PathMapGridController _pathMapGrid;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Animator animator;
        
        private void Awake()
        {
            CreatePanelSevice();
            CreateGameControlService();
            CreateGlobalAnimationService();
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
            service.Init(_draftGrid, _battleGrid, _dungeonGrid, _pathMapGrid);
            DontDestroyOnLoad(obj);
        }

        private void CreateGlobalAnimationService()
        {
            var obj = new GameObject("GlobalAnimationService");
            var service = obj.AddComponent<GlobalAnimationSevice>();
            service.Init(animator);
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
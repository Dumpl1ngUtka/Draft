using Grid.BattleGrid;
using Grid.DraftGrid;
using Grid.PathMapGrid;
using Grid.SelectDungeonGrid;
using Services.GameControlService;
using Services.GlobalAnimation;
using Services.PanelService;
using Services.SaveLoadSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bootstrappers
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Transform _gridContainer;
        [Header("Grids")]
        [SerializeField] private DraftGridController _draftGridPrefab;
        [SerializeField] private BattleGridController _battleGridPrefab;
        [SerializeField] private SelectDungeonGridController _dungeonGridPrefab;
        [SerializeField] private PathMapGridController _pathMapGridPrefab;
        [SerializeField] private Animator animator;
        
        private void Awake()
        {
            CreatePanelSevice();
            CreateGameControlService();
            CreateGlobalAnimationService();
            CreateSaveLoadService();
            
            GameControlService.Instance.ChangeGrid(_dungeonGridPrefab);
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
            service.Init(_gridContainer, _draftGridPrefab, _battleGridPrefab, _dungeonGridPrefab, _pathMapGridPrefab);
            DontDestroyOnLoad(obj);
        }

        private void CreateGlobalAnimationService()
        {
            var obj = new GameObject("GlobalAnimationService");
            var service = obj.AddComponent<GlobalAnimationSevice>();
            service.Init(animator);
            DontDestroyOnLoad(obj);
        }

        private void CreateSaveLoadService()
        {
            var dungeonRepositiory = new JsonSaveLoadRepository<LocationData>();
            var belongingRepositiory = new JsonSaveLoadRepository<BelongingData>();
            var runRepository = new JsonSaveLoadRepository<RunData>();
            
            var obj = new GameObject("SaveLoadService");
            var service = obj.AddComponent<SaveLoadService>();
            service.Init(dungeonRepositiory, belongingRepositiory, runRepository);
            DontDestroyOnLoad(obj);
        }
    }
}
using System.Collections.Generic;
using DungeonMap;
using Grid.BattleGrid;
using Grid.DraftGrid;
using Grid.PathMapGrid;
using Grid.SelectDungeonGrid;
using PathMap;
using Units;
using UnityEngine;

namespace Services.GameControlService
{
    public class GameControlService : MonoBehaviour
    {
        private GridStateMachine.GridStateMachine _gridStateMachine;
        
        public static GameControlService Instance {get; private set;}
        public DungeonInfo CurrentDungeonInfo { get;  set; }
        public RunInfo CurrentRunInfo { get;  set; }
        public List<Unit> PlayerUnits { get;  set; }

        public void Init(DraftGridController draftGrid,
            BattleGridController battleGrid,
            SelectDungeonGridController selectDungeonGrid,
            PathMapGridController pathMapGrid)
        {
            Instance = FindFirstObjectByType<GameControlService>();
            _gridStateMachine = new GridStateMachine.GridStateMachine(draftGrid,battleGrid, selectDungeonGrid, pathMapGrid);
        }

        private void Start()
        {
            _gridStateMachine.ChangeGrid(_gridStateMachine.DungeonGrid);
        }
    }
}

using System;
using System.Collections.Generic;
using Battle.Grid;
using Battle.Grid.CardParameter;
using Battle.Units;
using UnityEngine;

namespace Battle
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies;
        [SerializeField] private DraftGrid _draftGrid;
        [SerializeField] private BattleGrid _battleGrid;
        private List<PlayerUnit> _playerUnits = new List<PlayerUnit>();

        private void Awake()
        {
            _draftGrid.Initialize(this);
            _battleGrid.Initialize(this);
            _battleGrid.gameObject.SetActive(false);
            _draftGrid.gameObject.SetActive(true);
        }

        public void FinishDraft()
        {
            if (_draftGrid.FillCellsCount < 9)
                return;
            
            _draftGrid.gameObject.SetActive(false);
            _playerUnits = _draftGrid.GetUnits();
            _battleGrid.Fill(_playerUnits, _enemies);
            _battleGrid.gameObject.SetActive(true);
        }
    }
}

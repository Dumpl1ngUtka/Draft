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
        [SerializeField] private DraftGrid _draftGrid;
        [SerializeField] private BattleGrid _battleGrid;
        private List<Unit> _units = new List<Unit>();

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
            _units = _draftGrid.GetUnits();
            _battleGrid.Fill(_units);
            _battleGrid.gameObject.SetActive(true);
        }
    }
}

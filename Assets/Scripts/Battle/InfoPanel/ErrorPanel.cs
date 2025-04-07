using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.InfoPanel
{
    public class ErrorPanel : InfoPanel
    {
        private const float _destroyTime = 5f;
        
        [SerializeField] private Image _progressBar;
        
        private float _timer;

        protected override string GetString(string key)
        {
            var _testData = new Dictionary<string, string>()
            {
                {"teammete_error", "You can't use this ability on your teammates!"}, 
                {"line_index_mismatch_error", "You can swap targets from one line."}, 
                {"unit_null_error", "The cell should not be empty.[line_index_mismatch_error]"}, 
                {"team_index_mismatch_error", "Your teammates should be the target!"},
                {"draft_fill_cells_error", "You have to fill in all the cells!"},
                {"unit_not_ready_error", "Unit is not ready!"},
                {"no_player_unit_error", "You can only control your units."},
                {"target_dead_error", "Target is dead!"},
            };
            return _testData.GetValueOrDefault(key, key);
        }

        private void Start()
        {
            _timer = 0f;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            _progressBar.fillAmount = _timer / _destroyTime;
            if (_timer >= _destroyTime)
            {
                Destroy();
            }
        }
    }
}
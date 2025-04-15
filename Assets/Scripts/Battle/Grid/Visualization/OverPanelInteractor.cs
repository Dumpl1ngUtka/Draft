using System;
using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class OverPanelInteractor : GridCellInteractor
    {
        [SerializeField] private Image _overPanel;
        [SerializeField] private Sprite _defaultImage;
        
        protected override void ActiveChanged(bool isActive)
        {
            _overPanel.gameObject.SetActive(isActive);
        }

        public void SetColor(Color color)
        {
            _overPanel.sprite = _defaultImage;
            _overPanel.color = color;
        }

        protected override void UpdateInfo(Unit unit)
        {
        }
    }
}
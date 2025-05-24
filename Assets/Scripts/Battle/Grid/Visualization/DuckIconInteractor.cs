using System;
using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class DuckIconInteractor : GridCellInteractor
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _deadSprite;
        [SerializeField] private Sprite _emptySprite;

        protected override void UpdateInfo()
        {            
            if (_icon == null)
                return;
            
            _icon.sprite = Unit.Stats.IsDead? _deadSprite : Unit.Icon;
        }

        protected override void SetActive(bool isActive)
        {
            if (!isActive && _icon != null)
                _icon.sprite = _emptySprite;
        }
    }
}
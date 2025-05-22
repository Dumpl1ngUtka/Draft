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

        public override void UpdateInfo()
        {            
            base.UpdateInfo();
            if (_icon == null)
                return;
            
            _icon.sprite = Unit.Stats.IsDead? _deadSprite : Unit.Icon;
        }
    }
}
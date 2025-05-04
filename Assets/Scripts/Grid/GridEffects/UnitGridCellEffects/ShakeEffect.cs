using Battle.Grid.Visualization;
using UnityEngine;

namespace Grid.GridEffects.UnitGridCellEffects
{
    public class ShakeEffect : Effect
    {
        private float _minSize;
        private float _maxSize;
        private float _speed;
        private float _currentSize;
        private float _preferredSize;
        
        public ShakeEffect(UnitGridCellRenderer cell, bool isInfPlay, float duration, float minSize, float maxSize, float speed) 
            : base(cell, isInfPlay, duration)
        {
            _minSize = minSize;
            _maxSize = maxSize;
            _speed = speed;
        }

        protected override void EffectUpdate(float time)
        {
            var func = (Mathf.Sin(time * _speed) + 1) / 2;
            var size = Mathf.Lerp(_minSize, _maxSize,func);
            Renderer.transform.localScale = new Vector3(size, size, size);
        }
    }
}
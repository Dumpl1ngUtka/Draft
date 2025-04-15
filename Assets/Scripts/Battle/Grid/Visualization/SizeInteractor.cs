using System;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class SizeInteractor
    {
        private const float _sizeAnimationSpeed = 5f;
        private const float _sinFuncSpeed = 5f;
        private const float _sinFuncPower = 0.1f;
        
        [SerializeField] private Transform _transform;
        
        private float _preferredSize = 1f;
        private float _currentSize = 1f;
        private float _interactTimer;
        
        private bool _isDefaultSize => _preferredSize == 1f;
        
        public void SetSize(float size)
        {
            _preferredSize = size;
            if (!_isDefaultSize)
                _interactTimer = 0f;
        }

        public void SetSizeInstantly(float size)
        {
            _preferredSize = size;
            _currentSize = size;
            _transform.localScale = new Vector3(size, size, size);
        }

        public void Update()
        {
            var delta = 0f;
            if (_preferredSize > 1)
            {
                delta = (Mathf.Sin(_interactTimer) + 1) / 2 * _sinFuncPower;
                _interactTimer += Time.deltaTime * _sinFuncSpeed;
            }
            
            if (Mathf.Pow(_currentSize - _preferredSize, 2) > 0.001f || !_isDefaultSize)
            {
                _currentSize = Mathf.Lerp(_currentSize, _preferredSize, _sizeAnimationSpeed * Time.deltaTime);
                _transform.localScale = Vector3.one * (_currentSize+ delta);
            }
        }
    }
}
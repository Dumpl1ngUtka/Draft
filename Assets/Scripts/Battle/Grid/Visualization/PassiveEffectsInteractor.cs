using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class PassiveEffectsInteractor : GridCellInteractor
    {
        [SerializeField] private PassiveEffectCell _prefab;
        [SerializeField] private Transform _container;

        public void SetFields(PassiveEffectCell prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }
        
        protected override void UpdateInfo()
        {
            if (_container == null)
                return;
            
            ClearContainer();
            var effects = Unit.PassiveEffectsHolder.GetPassiveEffects();
            foreach (var effect in effects)
            {
                if (effect.Icon == null)
                    continue;
                
                var renderer = Object.Instantiate(_prefab, _container);
                renderer.Init(effect);
            }
        }

        protected override void SetActive(bool isActive)
        {
            if (_container != null)
                _container.gameObject.SetActive(isActive);
        }

        private void ClearContainer()
        {
            foreach(Transform child in _container.transform) 
                Object.Destroy(child.gameObject);
        }
    }
}
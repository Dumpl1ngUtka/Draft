using System;
using Battle.PassiveEffects;
using Battle.Units;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class PassiveEffectsInteractor : GridCellInteractor
    {
        [SerializeField] private PassiveEffectCell _prefab;
        [SerializeField] private Transform _container;

        protected override void ActiveChanged(bool isActive)
        {
            _container?.gameObject.SetActive(isActive);
        }

        protected override void UpdateInfo(Unit unit)
        {
            if (_container == null)
                return;
            
            ClearContainer();
            var effects = unit.PassiveEffectsHolder.GetPassiveEffects();
            foreach (var effect in effects)
            {
                var renderer = GameObject.Instantiate(_prefab, _container);
                renderer.Init(effect);
            }
        }

        private void ClearContainer()
        {
            foreach(Transform child in _container.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
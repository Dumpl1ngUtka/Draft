using System.Collections;
using Battle.Grid.Visualization;
using UnityEngine;

namespace Grid.GridEffects.UnitGridCellEffects
{
    public abstract class Effect
    {
        protected UnitGridCellRenderer Renderer;
        private float _timer;
        private readonly float _maxTime;
        private Coroutine _coroutine;
        
        protected Effect(UnitGridCellRenderer cell, bool isInfPlay, float duration)
        {
            Renderer = cell;
            _timer = 0;
            _maxTime = isInfPlay? float.MaxValue : duration;
            _coroutine = Renderer.StartCoroutine(PlayEffect());
        }

        private IEnumerator PlayEffect()
        {
            while (_timer < _maxTime)
            {
                EffectUpdate(_timer);
                _timer += Time.deltaTime;
                yield return null;
            } 
        }

        public void StopEffect()
        {
            Renderer.StopCoroutine(_coroutine);
        }

        protected abstract void EffectUpdate(float time);
    }
}
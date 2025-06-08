using System;
using System.Collections.Generic;
using Battle.PassiveEffects;
using Services.GlobalAnimation;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class OverPanelInteractor : GridCellInteractor
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private EffectAnimation _effectAnimationPrefab;
        [SerializeField] private Transform _frontEffectContainer;
        private Queue<AnimationClip> _animationQueue = new Queue<AnimationClip>();
        private bool _isPlaying;

        public void PlayEffectAnimation(PassiveEffect effect, TriggerType type)
        {
            var clip = effect.GetClipByType(type);
            if (clip != null) 
                PlayAnimation(clip);

            var effectClip = effect.GetClipByType(TriggerType.Effect);
            if (effectClip != null)
                InstantiateEffectAnimation(effectClip, effect);
        }

        private void InstantiateEffectAnimation(AnimationClip effectClip, PassiveEffect effect)
        {
            var instance = Object.Instantiate(_effectAnimationPrefab, _frontEffectContainer);
            instance.Init(effectClip, effect);
        }

        public void PlayAnimation(AnimationClip clip)
        {
            _animationQueue.Enqueue(clip);

            if (!_isPlaying) 
                PlayNextAnimation();
        }

        private void PlayNextAnimation()
        {
            if (_animationQueue.Count == 0)
            {
                _isPlaying = false;
                return;
            }

            _isPlaying = true;
            AnimationClip nextClip = _animationQueue.Dequeue();

            var controller = new AnimatorController();
            controller.AddLayer("Base Layer");
            var state = controller.AddMotion(nextClip, 0);

            _animator.runtimeAnimatorController = controller;
            
            Renderer.StartCoroutine(WaitForAnimationEnd(nextClip.length));
        }

        private System.Collections.IEnumerator WaitForAnimationEnd(float animationLength)
        {
            yield return new WaitForSeconds(animationLength);
            PlayNextAnimation();
        }

        protected override void UpdateInfo()
        {
            ;
        }

        protected override void SetActive(bool isActive)
        {
            ;
        }

        public void OnDestroy()
        {
            if (Renderer != null)
                Renderer.StopAllCoroutines();
        }
    }
}
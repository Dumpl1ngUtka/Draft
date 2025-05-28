using System;
using System.Collections.Generic;
using Services.GlobalAnimation;
using UnityEditor.Animations;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class OverPanelInteractor : GridCellInteractor
    {
        [SerializeField] private Animator _animator;
        private Queue<AnimationClip> _animationQueue = new Queue<AnimationClip>();
        private bool _isPlaying;

        public void PlayAnimation(AnimationClip clip)
        {
            _animationQueue.Enqueue(clip);

            if (!_isPlaying)
            {
                PlayNextAnimation();
            }
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
            
            GlobalAnimationSevice.Instance.StartCoroutine(WaitForAnimationEnd(nextClip.length));
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
    }
}
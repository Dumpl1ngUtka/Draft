using System;
using System.Collections.Generic;
using Battle.Units;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class OverPanelInteractor : GridCellInteractor
    {
        [SerializeField] private Image _animationImage;
        [SerializeField] private Animator _animator;
        [SerializeField] private Sprite _defaultImage;
        private PlayableGraph _playableGraph;
        private AnimationMixerPlayable _topLevelMixer;
        private AnimationClipPlayable _oneShotPlayable;
        private float _oneShotTimer = 0f;
        private List<AnimationClip> _animationQueue;

        public void SetColor(Color color)
        {
            //_animationImage.sprite = _defaultImage;
            //_animationImage.color = color;
        }

        protected override void UpdateInfo()
        {
        }

        protected override void SetActive(bool isActive)
        {
            
        }

        public void Init()
        {
            _playableGraph = PlayableGraph.Create("PieceAnimationSystem");

            AnimationPlayableOutput output = AnimationPlayableOutput.Create(_playableGraph, "Animation", _animator);
            _topLevelMixer = AnimationMixerPlayable.Create(_playableGraph, 1);
            output.SetSourcePlayable(_topLevelMixer);

            _playableGraph.GetRootPlayable(0).SetInputWeight(0, 1f);

            _playableGraph.Play();
            
            _animationQueue = new List<AnimationClip>();
        }

        public void Update()
        {
            base.TryUpdateInfo();
            if (!_oneShotPlayable.IsValid()) return;
            
            if (_oneShotTimer <= 0f)
            {
                if (_animationQueue.Count > 0)
                {
                    var clip = _animationQueue[0];
                    PlayOneShotAnimation(clip, clip.length);
                    _animationQueue.RemoveAt(0);
                }
                else
                {
                    InterruptOneShot();
                }
            }
            else
            {
                _oneShotTimer -= Time.deltaTime;
            }
        }

        public void PlayOneShotAnimation(AnimationClip animationClip, float animationTime)
        {
            if (_oneShotTimer > 0f)
            {
                _animationQueue.Add(animationClip);
                return;
            }

            InterruptOneShot();
            
            _oneShotTimer = animationTime;
            _oneShotPlayable = AnimationClipPlayable.Create(_playableGraph, animationClip);
            _oneShotPlayable.SetSpeed(animationClip.length / animationTime);
            _topLevelMixer.ConnectInput(0, _oneShotPlayable, 0);
            _topLevelMixer.SetInputWeight(0, 1f);
            _animationImage.enabled = true;
        }
        
        private void InterruptOneShot()
        {
            if (_oneShotPlayable.IsValid())
                DisconnectOneShotInput();
        }

        private void DisconnectOneShotInput()
        {
            _topLevelMixer.DisconnectInput(0);
            _playableGraph.DestroyPlayable(_oneShotPlayable);
            _animationImage.enabled = false;
        }

        public void OnDestroy()
        { 
            _playableGraph.Destroy();
        }
    }
}
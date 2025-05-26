using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Services.GlobalAnimation
{
    public class GlobalAnimationSevice : MonoBehaviour
    {
        private Image _animationImage;
        private PlayableGraph _playableGraph;
        private AnimationMixerPlayable _topLevelMixer;

        private AnimationClipPlayable _oneShotPlayable;
        private float _oneShotTimer = 0f;
        private Action _callback;
        public static GlobalAnimationSevice Instance { get; private set; }

        public void Init(Animator _animator)
        {
            Instance = FindFirstObjectByType<GlobalAnimationSevice>();
            
            SetAnimaitonImage(_animator);

            _playableGraph = PlayableGraph.Create("PieceAnimationSystem");

            AnimationPlayableOutput output = AnimationPlayableOutput.Create(_playableGraph, "Animation", _animator);
            _topLevelMixer = AnimationMixerPlayable.Create(_playableGraph, 1);
            output.SetSourcePlayable(_topLevelMixer);

            _playableGraph.GetRootPlayable(0).SetInputWeight(0, 1f);

            _playableGraph.Play();
        }

        private void SetAnimaitonImage(Animator _animator)
        {
            _animationImage = _animator.GetComponent<Image>();
            if (_animationImage == null)
                _animationImage = _animator.gameObject.AddComponent<Image>();
        }

        private void Update()
        {
            if (_oneShotPlayable.IsValid())
            {
                if (_oneShotTimer <= 0f)
                    InterruptOneShot();
                else
                    _oneShotTimer -= Time.deltaTime;
            }
        }

        public void PlayRandomTransitionAnimaton(bool isEnterAnimation, Action callback = null)
        {
            var path = "Animations/Transition/" + (isEnterAnimation? "Enter/" : "Exit/");
            var clips = Resources.LoadAll<AnimationClip>(path);
            var clip = clips[Random.Range(0, clips.Length)];
            PlayOneShotAnimation(clip, clip.length, callback);
        }

        public void PlayOneShotAnimation(AnimationClip animationClip, float animationTime, Action callback = null)
        {
            if (_oneShotPlayable.IsValid() && _oneShotPlayable.GetAnimationClip() == animationClip)
                return;

            InterruptOneShot();
            
            _oneShotTimer = animationTime;
            _oneShotPlayable = AnimationClipPlayable.Create(_playableGraph, animationClip);
            _oneShotPlayable.SetSpeed(animationClip.length / animationTime);
            _topLevelMixer.ConnectInput(0, _oneShotPlayable, 0);
            _topLevelMixer.SetInputWeight(0, 1f);
            //_animationImage.enabled = true;
            _callback = callback;
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
            //_animationImage.enabled = false;
            _callback?.Invoke();
        }

        private void OnDestroy()
        {
            if (_playableGraph.IsValid())
                _playableGraph.Destroy();
        }
    }
}
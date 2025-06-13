using Battle.PassiveEffects;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid.Visualization
{
    public class EffectAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _image;

        public void Init(AnimationClip effectClip, PassiveEffect effect)
        {
            var controller = new AnimatorController();
            controller.AddLayer("Base Layer");
            var state = controller.AddMotion(effectClip, 0);
            _animator.runtimeAnimatorController = controller;
            _image.color = effect.Color;
            
            effect.EffectRemoved += EffectRemoved;
        }

        private void EffectRemoved()
        {
            Destroy(this.gameObject);
        }
    }
}
using Battle.PassiveEffects;
using UnityEditor.Animations;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    public class EffectAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Init(AnimationClip effectClip, PassiveEffect effect)
        {
            var controller = new AnimatorController();
            controller.AddLayer("Base Layer");
            var state = controller.AddMotion(effectClip, 0);
            _animator.runtimeAnimatorController = controller;
            
            effect.EffectRemoved += EffectRemoved;
        }

        private void EffectRemoved()
        {
            Destroy(this.gameObject);
        }
    }
}
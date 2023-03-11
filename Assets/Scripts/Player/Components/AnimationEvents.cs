using System;
using UnityEngine;

namespace Player.Components
{
    public class AnimationEvents<T> : MonoBehaviour where T : CharacterComponent
    {
        protected T _component;
        
        private void Awake()
        {
            _component = GetComponentInParent<T>();
        }
        
        protected bool IsInAnimationTransition(int layerIndex = 0)
        {
            return _component.Animator.IsInTransition(layerIndex);
        }
    }
}
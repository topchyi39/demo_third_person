using UnityEngine;

namespace Player.Components.MovementComponents.Utility.Animations
{
    public class MovementAnimationEvents : AnimationEvents<MovementComponent>
    {
        public void OnMovementAnimationEnterEvent()
        {
            // if (IsInAnimationTransition())
            // {
            //     return;
            // }
            
            _component.OnAnimationEnterEvent();
        }
        
        public void OnMovementAnimationExitEvent()
        {
            if (IsInAnimationTransition())
            {
                return;
            }
            
            _component.OnAnimationExitEvent();
        }
        
        public void OnMovementAnimationTransitionEvent()
        {
            if (IsInAnimationTransition())
            {
                return;
            }

            _component.OnAnimationTransitionEvent();
        }
    }
}
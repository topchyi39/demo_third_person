using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates.MovingStates
{
    public class WalkState : MovingState
    {
        public WalkState(MovementComponent component) : base(component) { }
        
        public override void Enter()
        {
            base.Enter();

            _reusableData.SpeedModifier = _movementData.WalkData.SpeedModifier;
            _reusableData.TimeToReachRotation = _movementData.WalkData.TimeToReachRotation;
        }
        
        #region Input Callbacks
        
        protected override void MoveCanceled(InputAction.CallbackContext context)
        {
            _component.StateMachine.ChangeState(_component.StateMachine.WalkStoppingState);
            
            base.MoveCanceled(context);
        }

        #endregion
        
        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.WalkKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.WalkKey, false);
        }

        #endregion
    }
}
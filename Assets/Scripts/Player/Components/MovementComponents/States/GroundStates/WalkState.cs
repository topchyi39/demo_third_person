using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates
{
    public class WalkState : GroundState
    {
        public WalkState(MovementComponent component) : base(component)
        {
            _startAnimationAction = () =>
            {
                _component.Animator.SetBool(_animationData.WalkKey, true);
            };

            _endAnimationAction = () =>
            {
                _component.Animator.SetBool(_animationData.WalkKey, false);
            };
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _currentMovementStateData = _movementData.WalkData;
            _component.ReusableData.TimeToReachTargetRotation = _movementData.WalkData.TimeToReachRotation;
        }

        protected override void MoveCanceled(InputAction.CallbackContext context)
        {
            _component.StateMachine.ChangeState(_component.StateMachine.IdleState);
            
            base.MoveCanceled(context);
        }
    }
}
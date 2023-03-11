
using UnityEngine;

namespace Player.Components.MovementComponents.States.GroundStates.StartingStates
{
    public class WalkStartingState : StartingState
    {
        public WalkStartingState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _reusableData.SpeedModifier = _movementData.WalkData.StartSpeedModifier;
            _reusableData.TimeToReachRotation = _movementData.WalkData.TimeToReachRotation;
        }

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            _component.Animator.SetBool(_animationData.WalkKey, true);
            
            base.StartStateAnimation();
        }

        protected override void EndStateAnimation()
        {
            _component.Animator.SetBool(_animationData.WalkKey, false);
            
            base.EndStateAnimation();
        }

        public override void OnAnimationTransitionEvent()
        {
            if(_reusableData.MoveAxis != Vector2.zero)
                _component.StateMachine.ChangeState(_component.StateMachine.WalkState);
            else
            {
                _component.StateMachine.ChangeState(_component.StateMachine.IdleState);
            }
        }

        #endregion
    }
}
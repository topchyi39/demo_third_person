using UnityEditor.Rendering;
using UnityEngine;

namespace Player.Components.MovementComponents.States.GroundStates.LandingStates
{
    public class LightLandState : LandingState
    {
        public LightLandState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            if(_reusableData.MoveAxis == Vector2.zero)
                ResetVelocity();
            else
            {
                var speedModifier = _groundedData.WalkData.SpeedModifier;
                if (!_reusableData.ShouldWalk)
                    speedModifier = _groundedData.JogData.SpeedModifier;

                _reusableData.SpeedModifier = speedModifier;
            }
        }

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.LightLandingKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.LightLandingKey, false);
        }
        
        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            if (_reusableData.MoveAxis != Vector2.zero)
            {
                ChangeToMovingState();
                return;
            }
            
            if(_reusableData.ShouldCrouch)
                _component.StateMachine.ChangeState(_component.StateMachine.CrouchIdleState);
            else
                _component.StateMachine.ChangeState(_component.StateMachine.IdleState);
        }

        protected override void ChangeToMovingState()
        {
            BaseMoveState state;
            
            if(_reusableData.ShouldCrouch)
                state = _component.StateMachine.CrouchState;
            else if(_reusableData.ShouldWalk)
                state = _component.StateMachine.WalkState;
            else if (_reusableData.ShouldDash)
                state = _component.StateMachine.DashState;
            else
                state = _component.StateMachine.JogState;
            
            _component.StateMachine.ChangeState(state);
        }

        #endregion
    }
}
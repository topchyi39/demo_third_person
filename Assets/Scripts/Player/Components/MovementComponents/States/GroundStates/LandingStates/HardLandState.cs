using UnityEngine;

namespace Player.Components.MovementComponents.States.GroundStates.LandingStates
{
    public class HardLandState : LandingState
    {
        public HardLandState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            _component.CharacterInput.MoveAxis.Disable();
            ResetVelocity();
        }
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            ResetHorizontalVelocity();
        }

        public override void Exit()
        {
            base.Exit();
            
            _component.CharacterInput.MoveAxis.Enable();
        }

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.HardLandingKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.HardLandingKey, false);
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            if (_reusableData.MoveAxis != Vector2.zero)
            {
                ChangeToMovingState();
                return;
            }
            
            _component.StateMachine.ChangeState(_component.StateMachine.IdleState);
        }

        #endregion
    }
}
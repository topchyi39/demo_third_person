using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates
{
    public class IdleState : GroundState
    {
        public IdleState(MovementComponent component) : base(component)
        {
            _startAnimationAction = () =>
            {
                _component.Animator.SetBool(_animationData.IdleKey, true);
            };

            _endAnimationAction = () =>
            {
                _component.Animator.SetBool(_animationData.IdleKey, false);
            };
        }

        public override void Enter()
        {
            base.Enter();

            _component.ReusableData.SpeedModifier = 0f;
        }
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if (!IsMovingHorizontally())
            {
                return;
            }

            ResetVelocity();
        }

        protected override void MovePerformed(InputAction.CallbackContext context)
        {
            ChangeToMovingState();
            
            base.MovePerformed(context);
        }
        
        private void ChangeToMovingState()
        {
            _component.StateMachine.ChangeState(_component.StateMachine.WalkState);
        }
    }
}
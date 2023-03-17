using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates.MovingStates
{
    public class DashState : MovingState
    {
        public DashState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            _reusableData.SpeedModifier = _groundedData.DashData.SpeedModifier;
            _reusableData.JumpForce = _airborneData.JumpData.StrongForce;
            _reusableData.TimeToReachRotation = _groundedData.DashData.TimeToReachRotation;
        }

        public override void Exit()
        {
            base.Exit();
            ResetDash();
        }

        #region Input Callbacks

        protected override async void MoveCanceled(InputAction.CallbackContext context)
        {
            base.MoveCanceled(context);
            
            await Task.Delay(250);
            
            if(_reusableData.MoveAxis == Vector2.zero)
                _component.StateMachine.ChangeState(_component.StateMachine.DashStoppingState);
        }

        protected override void DashPerformed(InputAction.CallbackContext context)
        {
            base.DashPerformed(context);

            _reusableData.ShouldDash = false;
            BaseMoveState state;

            if (_reusableData.ShouldWalk)
                state = _component.StateMachine.WalkState;
            else
                state = _component.StateMachine.JogState;

            _component.StateMachine.ChangeState(state);
        }

        #endregion
        
        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.DashKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.DashKey, false);
        }

        #endregion
    }
}
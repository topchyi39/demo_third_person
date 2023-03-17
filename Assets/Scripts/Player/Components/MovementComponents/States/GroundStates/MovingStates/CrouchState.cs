using System.Threading.Tasks;
using Player.Components.MovementComponents.States.GroundStates.MovingStates;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates.CrouchingStates
{
    public class CrouchState : WalkState
    {
        private bool _shouldWalk;
        
        public CrouchState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            _shouldWalk = _reusableData.ShouldWalk;
            _reusableData.ShouldWalk = true;
            _component.ResizableCollider.UpdateSetting(_groundedData.CrouchCapsuleSettings);
        }

        public override void Exit()
        {
            base.Exit();
            _reusableData.ShouldWalk = _shouldWalk;
            _component.ResizableCollider.UpdateSetting(_groundedData.DefaultCapsuleSettings);
        }
        
        protected override void ChangeToMovingState()
        {
            BaseMoveState state;
            _reusableData.ShouldWalk = _shouldWalk;
            
            if(_reusableData.ShouldWalk)
                state = _component.StateMachine.WalkState;
            else
            {
                if (_reusableData.ShouldDash)
                    state = _component.StateMachine.DashState;
                else
                    state = _component.StateMachine.JogState;
            }
            
            _component.StateMachine.ChangeState(state);
        }

        #region Input Callbacks

        protected override async void ToStopping()
        {
            await Task.Delay(250);
            
            if(_reusableData.MoveAxis == Vector2.zero)
                _component.StateMachine.ChangeState(_component.StateMachine.CrouchStoppingState);
        }

        protected override void WalkToggled(InputAction.CallbackContext context) { }

        protected override void CrouchPerformed(InputAction.CallbackContext context)
        {
            _reusableData.ShouldCrouch = false;
            
            if(_reusableData.MoveAxis == Vector2.zero)
                _component.StateMachine.ChangeState(_component.StateMachine.IdleState);
            else
            {
                ChangeToMovingState();
            }
        }

        #endregion
        

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.CrouchKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.CrouchKey, false);
        }

        #endregion
    }
}
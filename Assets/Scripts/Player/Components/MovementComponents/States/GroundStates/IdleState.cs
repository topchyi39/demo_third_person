using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates
{
    public class IdleState : GroundState
    {
        public IdleState(MovementComponent component) : base(component) { }
        
        public override void Enter()
        {
            base.Enter();

            _reusableData.SpeedModifier = 0f;
            _reusableData.JumpForce = _airborneData.JumpData.StationaryForce;
            _component.ResizableCollider.UpdateSetting(_groundedData.DefaultCapsuleSettings);
        }
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            ResetHorizontalVelocity();
        }

        #region Input Callbacks

        protected override void MovePerformed(InputAction.CallbackContext context)
        {
            var axis = context.ReadValue<Vector2>();
            
            _component.Animator.SetFloat(_animationData.VerticalKey, axis.y);
            _component.Animator.SetFloat(_animationData.HorizontalKey, axis.x);
            
            ChangeToMovingState();
            base.MovePerformed(context);
        }

        protected override void CrouchPerformed(InputAction.CallbackContext context)
        {
            base.CrouchPerformed(context);
            
            if (_reusableData.ShouldCrouch)
                _component.StateMachine.ChangeState(_component.StateMachine.CrouchIdleState);
        }

        protected override void RollPerformed(InputAction.CallbackContext context) { }

        protected override void DashPerformed(InputAction.CallbackContext context)
        {
            base.DashPerformed(context);
            _reusableData.ShouldDash = true;
        }

        protected override void DashCanceled(InputAction.CallbackContext context)
        {
            base.DashCanceled(context);
            _reusableData.ShouldDash = false;
        }

        #endregion

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.IdleKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.IdleKey, false);
        }

        #endregion
    }
}
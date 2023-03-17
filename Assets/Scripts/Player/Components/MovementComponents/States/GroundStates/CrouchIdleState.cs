using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates.CrouchingStates
{
    public class CrouchIdleState : IdleState
    {
        private bool _shouldWalk;

        public CrouchIdleState(MovementComponent component) : base(component)
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
            _component.StateMachine.ChangeState(_component.StateMachine.CrouchState);
        }

        #region Input Callbacks

        protected override void CrouchPerformed(InputAction.CallbackContext context)
        {
            _reusableData.ShouldCrouch = false;
            _component.StateMachine.ChangeState(_component.StateMachine.IdleState);
        }

        #endregion

        #region Animation Callbacks

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
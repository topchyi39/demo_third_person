using Player.Components.MovementComponents.States.GroundStates.StoppingStates;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates.CrouchingStates
{
    public class CrouchStoppingState : WalkStoppingState
    {
        private float _startTime;
        
        public CrouchStoppingState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _startTime = Time.time;
            _component.ResizableCollider.UpdateSetting(_groundedData.CrouchCapsuleSettings);
        }

        public override void Exit()
        {
            base.Exit();
            _component.ResizableCollider.UpdateSetting(_groundedData.DefaultCapsuleSettings);
        }

        public override void Update()
        {
            base.Update();

            if (_startTime + 0.1f < Time.time) 
                ChangeToIdleState();
        }

        protected override void ChangeToIdleState()
        {
            _component.StateMachine.ChangeState(_component.StateMachine.CrouchIdleState);
        }

        #region Input Callbacks

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
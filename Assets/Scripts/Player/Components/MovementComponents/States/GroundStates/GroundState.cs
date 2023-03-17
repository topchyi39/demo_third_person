using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates
{
    public class GroundState : BaseMoveState
    {
        public GroundState(MovementComponent component) : base(component)
        {
            
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            Float();
        }

        #region Physic Methods
        
        protected override void OnGroundExit(Collider collider)
        {
            if (IsThereGroundUnderneath())
            {
                return;
            }
            
            var capsuleColliderCenterInWorldSpace = _component.ResizableCollider.CapsuleColliderData.Collider.bounds.center;

            var downwardsRayFromCapsuleBottom = new Ray(capsuleColliderCenterInWorldSpace - _component.ResizableCollider.CapsuleColliderData.ColliderVerticalExtents, Vector3.down);

            if (!Physics.Raycast(downwardsRayFromCapsuleBottom, out _, _groundedData.DistanceToFall, _component.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                OnFall();
            }
        }

        private bool IsThereGroundUnderneath()
        {
            var triggerColliderData = _component.ResizableCollider.ColliderData;

            var groundColliderCenterInWorldSpace = triggerColliderData.GroundCheckCollider.bounds.center;

            var overlappedGroundColliders = Physics.OverlapBox(groundColliderCenterInWorldSpace, 
                triggerColliderData.Extends, 
                triggerColliderData.GroundCheckCollider.transform.rotation, 
                _component.LayerData.GroundLayer, QueryTriggerInteraction.Ignore);

            return overlappedGroundColliders.Length > 0;
        }

        protected void OnFall()
        {
            _component.StateMachine.ChangeState(_component.StateMachine.FallState);
        }

        #endregion

        #region Input Callbacks

        protected override void AddInputCallback()
        {
            base.AddInputCallback();

            _component.CharacterInput.Jump.performed += JumpPerformed;
            _component.CharacterInput.Dash.performed += DashPerformed;
            _component.CharacterInput.Dash.canceled += DashCanceled;
            _component.CharacterInput.Roll.performed += RollPerformed;
            _component.CharacterInput.Crouch.performed += CrouchPerformed;
        }

        protected override void RemoveInputCallback()
        {
            base.RemoveInputCallback();
            
            _component.CharacterInput.Jump.performed -= JumpPerformed;
            _component.CharacterInput.Dash.performed -= DashPerformed;
            _component.CharacterInput.Dash.canceled -= DashCanceled;
            _component.CharacterInput.Roll.performed -= RollPerformed;
            _component.CharacterInput.Crouch.performed -= CrouchPerformed;
        }

        protected virtual void CrouchPerformed(InputAction.CallbackContext context)
        {
            _reusableData.ShouldCrouch = !_reusableData.ShouldCrouch;
        }

        protected virtual void JumpPerformed(InputAction.CallbackContext context)
        {
            _component.StateMachine.ChangeState(_component.StateMachine.JumpState);
        }
        
        protected virtual void RollPerformed(InputAction.CallbackContext context)
        {
            _component.StateMachine.ChangeState(_component.StateMachine.RollingState);
        }

        #endregion

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.GroundedKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.GroundedKey, false);
        }

        #endregion
    }
}
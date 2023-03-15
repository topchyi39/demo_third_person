using UnityEngine;

namespace Player.Components.MovementComponents.States.AirborneStates
{
    public class JumpState : AirborneState
    {
        private bool _shouldRotate;
        private bool _canStartFall;
        private bool _jumped;
        
        public JumpState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _reusableData.SpeedModifier = 0f;
            _reusableData.TimeToReachRotation = _airborneData.JumpData.TimeToReachRotation;
            _reusableData.MovementDecelerationForce = _airborneData.JumpData.DecelerationForce;
            
            _shouldRotate = _reusableData.MoveAxis != Vector2.zero;
            _component.Animator.SetFloat(_animationData.VerticalKey, _reusableData.MoveAxis.y);
            _component.Animator.SetFloat(_animationData.HorizontalKey, _reusableData.MoveAxis.x);
        }

        public override void Exit()
        {
            base.Exit();

            _canStartFall = false;
            _jumped = false;
        }

        public override void Update()
        {
            base.Update();
            if (!_jumped) return;

            if (!_canStartFall && IsMovingUp())
            {
                _canStartFall = true;
            }

            if (!_canStartFall || IsMovingUp())
            {
                return;
            }
            
            _component.StateMachine.ChangeState(_component.StateMachine.FallState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!_jumped)
            {
                Float();
                return;
            }
            
            if (_shouldRotate)
            {
                SmoothRotateTowards();
            }

            if (IsMovingUp())
            {
                DecelerateVertically();
            }
        }

        private void Jump()
        {
            var jumpForce = _reusableData.JumpForce;

            var jumpDirection = CalculateDirection();

            if (_shouldRotate)
            {
                UpdateTargetRotate();
                // jumpDirection = Quaternion.Euler(0f, _reusableData.TargetRotation.y, 0f) * Vector3.forward;
            }
            
            jumpForce.x *= jumpDirection.x;
            jumpForce.z *= jumpDirection.z;
            jumpForce = GetJumpForceOnSlope(jumpForce);
            
            // ResetVelocity();
            _component.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
        }

        private Vector3 GetJumpForceOnSlope(Vector3 jumpForce)
        {
            var capsuleColliderCenterInWorldSpace = _component.ResizableCollider.CapsuleColliderData.Collider.bounds.center;

            var downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, _airborneData.JumpData.JumpToGroundRayDistance, _component.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                var groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

                if (IsMovingUp())
                {
                    var forceModifier = _airborneData.JumpData.OnSlopeUpwards.Evaluate(groundAngle);

                    jumpForce.x *= forceModifier;
                    jumpForce.z *= forceModifier;
                }

                if (IsMovingDown())
                {
                    var forceModifier = _airborneData.JumpData.OnSlopeDownwards.Evaluate(groundAngle);

                    jumpForce.y *= forceModifier;
                }
            }

            return jumpForce;
        }

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.JumpKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            _component.Animator.SetBool(_animationData.JumpKey, false);
        }

        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();
            
            Debug.LogError(1);
            _jumped = true;
            Jump();
        }

        #endregion
    }
}
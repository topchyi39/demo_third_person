using System;
using FiniteStateMachine;
using Player.Components.MovementComponents.Data;
using Player.Components.MovementComponents.Data.AirborneData;
using Player.Components.MovementComponents.Data.GroundedData;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Player.Components.MovementComponents.States
{
    public class BaseMoveState : State
    {
        protected MovementComponent _component;
        protected MovementData _movementData;
        protected GroundedData _groundedData;
        protected AirborneData _airborneData;
        protected MovementAnimationData _animationData;
        protected Transform _transform;

        protected ReusableData _reusableData;
        
        public BaseMoveState(MovementComponent component)
        {
            _component = component;
            _reusableData = _component.ReusableData;
            _movementData = _component.MovementData;
            _animationData = _component.AnimationData;
            _transform = _component.transform;

            _groundedData = _movementData.GroundedData;
            _airborneData = _movementData.AirborneData;
        }
        
        public override void Enter()
        {
            AddInputCallback();
            StartStateAnimation();
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            Move();
        }

        public override void Exit()
        {
            RemoveInputCallback();
            EndStateAnimation();
        }

        #region Moving and Rotating
        
        private void Move()
        {
            if(_component.ReusableData.MoveAxis == Vector2.zero || _reusableData.SpeedModifier == 0f) return;
            
            var direction = CalculateDirection();

            Rotate();
            
            var currentVelocity = GetHorizontalVelocity();
            
            _component.Rigidbody.AddForce(direction * GetMovementSpeed() - currentVelocity, ForceMode.VelocityChange);
        }

        /// <summary>
        /// Calculate direction from camera forward
        /// </summary>
        /// <returns></returns>
        protected Vector3 CalculateDirection(bool absVerticalAxis = false, bool inverseHorizontal = false)
        {
            var verticalAxis = _reusableData.MoveAxis.y;
            
            if (absVerticalAxis)
                verticalAxis = Mathf.Abs(verticalAxis);

            var horizontalAxis = _reusableData.MoveAxis.x;
            
            if (inverseHorizontal)
                horizontalAxis = -horizontalAxis;
            
            var zVelocity = _component.CameraTransform.forward * verticalAxis;
            var xVelocity = _component.CameraTransform.right * horizontalAxis;
            
            var velocity = zVelocity + xVelocity;
            
            velocity.y = 0;

            return velocity;
        }

        /// <summary>
        /// Rotate character to input direction by direction
        /// </summary>
        /// <param name="direction"></param>
        protected virtual void Rotate()
        {
            UpdateTargetRotate();

            SmoothRotateTowards();
        }

        protected void UpdateTargetRotate()
        {
            var directionAngle = _component.CameraTransform.eulerAngles.y;

            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            if (Math.Abs(directionAngle - _reusableData.TargetRotation.y) > 0.01f)
            {
                UpdateTargetRotationData(directionAngle);
            }
        }

        /// <summary>
        /// Store target direction
        /// </summary>
        /// <param name="targetAngle"></param>
        private void UpdateTargetRotationData(float targetAngle)
        {
            _reusableData.TargetRotation.y = targetAngle;
            _reusableData.DampedTargetRotationPassedTime = 0f;
        }
        
        /// <summary>
        /// Smooth rotate to target rotation
        /// </summary>
        protected void SmoothRotateTowards()
        {
            var currentAngle = _component.Rigidbody.rotation.eulerAngles.y;
            var targetAngle = _reusableData.TargetRotation.y;

            var smoothAngle = Mathf.SmoothDampAngle(
                currentAngle,
                targetAngle,
                ref _reusableData.DampedTargetRotation.y,
                _reusableData.TimeToReachRotation.y - _reusableData.DampedTargetRotationPassedTime);

            _reusableData.DampedTargetRotationPassedTime += Time.deltaTime;
            var targetRotation = Quaternion.Euler(0, smoothAngle, 0);
            
            _component.Rigidbody.MoveRotation(targetRotation);
        }

        protected void RotateToDirection(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            var targetRotation = Quaternion.Euler(0, directionAngle, 0);
            
            _component.Rigidbody.MoveRotation(targetRotation);
        }
        
        protected void Float()
        {
            var origin = _component.ResizableCollider.WorldCapsuleCentre;
            var direction = Vector3.down;
            var rayDistance = _component.ResizableCollider.SlopeData.FloatRayDistance;
            if (Physics.Raycast(origin, direction, out var hit, rayDistance, _component.LayerData.GroundLayer,
                    QueryTriggerInteraction.Ignore))
            {
                var groundAngle = Vector3.Angle(hit.normal, -direction);
               
                var slopeModifier = SetSlope(groundAngle);

                if (slopeModifier == 0f)
                {
                    var hitDirection = hit.normal;
                    hitDirection.y = 0;
                    var directionAngle = Vector3.Angle(hitDirection, _component.Rigidbody.transform.forward);
                    if (directionAngle < 90)
                        _reusableData.SlopeSpeedModifier = 1f;
                }
                
                var distanceToFloatPoint =
                    _component.ResizableCollider.LocalCapsuleCentre.y * _component.transform.localScale.y -
                    hit.distance;
                
                
                if (distanceToFloatPoint == 0f) return;
                
                var amountToLift = distanceToFloatPoint * _component.ResizableCollider.SlopeData.StepReachForce - GetVerticalVelocity().y;
                var liftForce = new Vector3(0f, amountToLift, 0f);
                _component.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }
        
        private float SetSlope(float groundAngle)
        {
            var slope = _movementData.Slope.Evaluate(groundAngle);

            _reusableData.SlopeSpeedModifier = slope;
            
            return slope;
        }
        
        protected float GetMovementSpeed(bool withSlope = true)
        {
            var speed = _component.MovementData.BaseSpeed * _reusableData.SpeedModifier;

            if (withSlope)
            {
                speed *= _reusableData.SlopeSpeedModifier;
            }
            
            return speed;
        }
     
        protected Vector3 GetVerticalVelocity()
        {
            var velocity = _component.Rigidbody.velocity;
            velocity.z = 0;
            velocity.x = 0;
            return velocity;
        }

        protected Vector3 GetHorizontalVelocity()
        {
            var velocity = _component.Rigidbody.velocity;
            velocity.y = 0;
            
            return velocity;
        }
        
        protected void ResetVelocity()
        {
            _component.Rigidbody.velocity = Vector3.zero;
        }
        
        protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
        {
            var horizontalVelocity = GetHorizontalVelocity();

            var horizontalMovement = new Vector2(horizontalVelocity.x, horizontalVelocity.z);

            return horizontalMovement.magnitude > minimumMagnitude;
        }
        
        protected void DecelerateHorizontally()
        {
            var horizontalVelocity = GetHorizontalVelocity();

            _component.Rigidbody.AddForce(-horizontalVelocity * _reusableData.MovementDecelerationForce, ForceMode.Acceleration);
        }

        protected void DecelerateVertically()
        {
            var verticalVelocity = GetVerticalVelocity();

            _component.Rigidbody.AddForce(-verticalVelocity * _reusableData.MovementDecelerationForce,
                ForceMode.Acceleration);
        }

        protected void ResetHorizontalVelocity()
        {
            _component.Rigidbody.velocity = GetVerticalVelocity();
        }
        
        protected bool IsMovingUp(float minimumVelocity = 0.01f)
        {
            return GetVerticalVelocity().y > minimumVelocity;
        }

        protected bool IsMovingDown(float minimumVelocity = 0.01f)
        {
            return GetVerticalVelocity().y < -minimumVelocity;
        }

        protected void ResetVerticalVelocity()
        {
            _component.Rigidbody.velocity = GetHorizontalVelocity();
        }

        protected virtual void ResetDash()
        {
            _reusableData.ShouldDash = false;
        }
        
        #endregion
        
        protected virtual void ChangeToMovingState()
        {
            BaseMoveState state;
            
            
            if(_reusableData.ShouldWalk)
                state = _component.StateMachine.WalkStartingState;
            else
            {
                if (_reusableData.ShouldDash)
                    state = _component.StateMachine.DashStartingState;
                else
                    state = _component.StateMachine.JogStartingState;
            }
            
            _component.StateMachine.ChangeState(state);
        }

        protected virtual void ChangeToIdleState()
        {
            _component.StateMachine.ChangeState(_component.StateMachine.IdleState);

        }

        #region Physic Methods

        public override void OnTriggerEnter(Collider other)
        {
            if (!_component.LayerData.IsGroundLayer(other.gameObject.layer)) return;
            
            OnGroundEnter(other);
        }

        public override void OnTriggerExit(Collider other)
        {
            if (!_component.LayerData.IsGroundLayer(other.gameObject.layer)) return;

            OnGroundExit(other);
        }
        
        protected virtual void OnGroundEnter(Collider collider)
        {
        }

        protected virtual void OnGroundExit(Collider collider)
        {
        }
        
        #endregion

        #region Input Callbacks

        protected virtual void AddInputCallback()
        {
            _component.CharacterInput.MoveAxis.performed += MovePerformed;
            _component.CharacterInput.MoveAxis.canceled += MoveCanceled;

            _component.CharacterInput.WalkToggle.performed += WalkToggled;
        }
        
        protected virtual void RemoveInputCallback()
        {
            _component.CharacterInput.MoveAxis.performed -= MovePerformed;
            _component.CharacterInput.MoveAxis.canceled -= MoveCanceled;
            
            _component.CharacterInput.WalkToggle.performed -= WalkToggled;
        }

        protected virtual void MovePerformed(InputAction.CallbackContext context)
        {
            _reusableData.CanUpdateMoveAnimation = true;
        }

        protected virtual void MoveCanceled(InputAction.CallbackContext context)
        {
            _reusableData.CanUpdateMoveAnimation = false;
        }

        protected virtual void WalkToggled(InputAction.CallbackContext context)
        {
            _reusableData.ShouldWalk = !_reusableData.ShouldWalk;
        }

        protected virtual void DashPerformed(InputAction.CallbackContext context)
        {
        }
        
        protected virtual void DashCanceled(InputAction.CallbackContext context)
        {
        }

        #endregion

        #region Animation Methods

        protected virtual void StartStateAnimation() { }
        protected virtual void EndStateAnimation() { }

        public override void OnAnimationEnterEvent() { }
        public override void OnAnimationExitEvent() { }
        public override void OnAnimationTransitionEvent() { }

        #endregion
    }
}
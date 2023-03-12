using System;
using FiniteStateMachine;
using Player.Components.MovementComponents.Data;
using Player.Components.MovementComponents.States.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Player.Components.MovementComponents.States
{
    public class BaseMoveState : State
    {
        protected MovementComponent _component;
        protected MovementData _movementData;
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

            Rotate(direction);
            
            var currentVelocity = GetHorizontalVelocity();
            
            _component.Rigidbody.AddForce(direction * GetMovementSpeed() - currentVelocity, ForceMode.VelocityChange);
        }

        /// <summary>
        /// Calculate direction from camera forward
        /// </summary>
        /// <returns></returns>
        protected Vector3 CalculateDirection()
        {
            var zVelocity = _component.CameraTransform.forward * _reusableData.MoveAxis.y;
            var xVelocity = _component.CameraTransform.right * _reusableData.MoveAxis.x;
            
            var velocity = zVelocity + xVelocity;
            
            velocity.y = 0;

            return velocity;
        }

        /// <summary>
        /// Rotate character to input direction by direction
        /// </summary>
        /// <param name="direction"></param>
        protected void Rotate(Vector3 direction)
        {
            // float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var directionAngle = _component.CameraTransform.eulerAngles.y;

            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }
            
            if (Math.Abs(directionAngle - _reusableData.TargetRotation.y) > 0.01f)
            {
                UpdateTargetRotationData(directionAngle);
            }

            RotateTowards();

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
        protected void RotateTowards()
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

        protected float GetMovementSpeed()
        {
            return _component.MovementData.BaseSpeed * _reusableData.SpeedModifier;
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
            var playerHorizontalVelocity = GetHorizontalVelocity();

            _component.Rigidbody.AddForce(-playerHorizontalVelocity * _reusableData.MovementDecelerationForce, ForceMode.Acceleration);
        }

        protected void ResetHorizontalVelocity()
        {
            var velocity = _component.Rigidbody.velocity;
            velocity.z = 0;
            velocity.x = 0;
            _component.Rigidbody.velocity = velocity;
        }
        
        #endregion
        
        protected void ChangeToMovingState()
        {
            BaseMoveState state;
            
            
            if(_reusableData.ShouldWalk)
                state = _component.StateMachine.WalkStartingState;
            else
            {
                state = _component.StateMachine.JogStartingState;
            }
            
            _component.StateMachine.ChangeState(state);
        }
        
        #region Input Callbacks

        protected virtual void AddInputCallback()
        {
            _component.Input.MoveAxis.performed += MovePerformed;
            _component.Input.MoveAxis.canceled += MoveCanceled;

            _component.Input.WalkToggle.performed += WalkToggled;
        }


        protected virtual void RemoveInputCallback()
        {
            _component.Input.MoveAxis.performed -= MovePerformed;
            _component.Input.MoveAxis.canceled -= MoveCanceled;
            
            _component.Input.WalkToggle.performed -= WalkToggled;
        }

        protected virtual void MovePerformed(InputAction.CallbackContext context) { }

        protected virtual void MoveCanceled(InputAction.CallbackContext context) { }

        protected virtual void WalkToggled(InputAction.CallbackContext context)
        {
            _reusableData.ShouldWalk = !_reusableData.ShouldWalk;
        }

        #endregion

        #region Animation Mathods

        protected virtual void StartStateAnimation() { }
        protected virtual void EndStateAnimation() { }

        public virtual void OnAnimationEnterEvent() { }
        public virtual void OnAnimationExitEvent() { }
        public virtual void OnAnimationTransitionEvent() { }

        #endregion
    }
}
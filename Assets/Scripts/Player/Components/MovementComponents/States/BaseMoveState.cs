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
        protected MovementStateData _currentMovementStateData;
        protected Transform _transform;

        protected ReusableData _reusableData;

        protected Action _startAnimationAction;
        protected Action _endAnimationAction;
        
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
            if(_component.ReusableData.MoveAxis == Vector2.zero || _currentMovementStateData.SpeedModifier == 0f) return;
            
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
            var zVelocity = _component.CameraTransform.forward * _component.ReusableData.MoveAxis.y;
            var xVelocity = _component.CameraTransform.right * _component.ReusableData.MoveAxis.x;
            
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
            
            if (Math.Abs(directionAngle - _component.ReusableData.TargetRotation.y) > 0.01f)
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
            _component.ReusableData.TargetRotation.y = targetAngle;
            _component.ReusableData.DampedTargetRotationPassedTime = 0f;
        }
        
        /// <summary>
        /// Smooth rotate to target rotation
        /// </summary>
        private void RotateTowards()
        {
            var currentAngle = _component.Rigidbody.rotation.eulerAngles.y;
            var targetAngle = _component.ReusableData.TargetRotation.y;

            var smoothAngle = Mathf.SmoothDampAngle(
                currentAngle,
                targetAngle,
                ref _component.ReusableData.DampedTargetRotation.y,
                _currentMovementStateData.TimeToReachRotation.y - _component.ReusableData.DampedTargetRotationPassedTime);

            _component.ReusableData.DampedTargetRotationPassedTime += Time.deltaTime;
            var targetRotation = Quaternion.Euler(0, smoothAngle, 0);
            
            _component.Rigidbody.MoveRotation(targetRotation);
        }

        protected float GetMovementSpeed()
        {
            return _component.MovementData.BaseSpeed * _currentMovementStateData.SpeedModifier;
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
        
        #endregion

        #region Input Callbacks

        protected virtual void AddInputCallback()
        {
            _component.Input.MoveAxis.performed += MovePerformed;
            _component.Input.MoveAxis.canceled += MoveCanceled;
        }


        protected virtual void RemoveInputCallback()
        {
            _component.Input.MoveAxis.performed -= MovePerformed;
            _component.Input.MoveAxis.canceled -= MoveCanceled;
        }

        protected virtual void MovePerformed(InputAction.CallbackContext context) { }

        protected virtual void MoveCanceled(InputAction.CallbackContext context) { }
        
        #endregion

        #region Animation Mathods

        protected virtual void StartStateAnimation()
        {
            _startAnimationAction?.Invoke();
        }

        protected virtual void EndStateAnimation()
        {
            _endAnimationAction?.Invoke();
        }

        #endregion
    }
}
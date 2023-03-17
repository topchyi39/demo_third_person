using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.CameraComponents
{
    [Serializable]
    public class RecenterSettings
    {
        
    }
    
    public class CameraComponent : CharacterComponent
    {
        [SerializeField] private Transform followTarget;
        
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private MoveSettings moveSettings;
        [SerializeField] private ZoomSettings zoomSettings;
        
        private Cinemachine3rdPersonFollow _bodyTransposer;
        
        private Vector2 _mouseLook;
        private float _sensitivity;
        private float _zoomDelta;
        private float _currentTargetDistance;
        
        private float xRotation;
        private float yRotation;

        [field: SerializeField] public bool CanRecenter { get; set; }
        
        public override void SetupAction()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _currentTargetDistance = zoomSettings.DefaultDistance;
            _bodyTransposer = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            
            characterInput.KeyboardActivated += KeyboardActivated;
            characterInput.GamepadActivated += GamepadActivated;
            
            if(characterInput.IsGamepadActivated)
                GamepadActivated();
            else
                KeyboardActivated();
        }

        public override void ExecuteUpdate()
        {
            _mouseLook = characterInput.Look.ReadValue<Vector2>();
            _zoomDelta = -characterInput.Zoom.ReadValue<float>() * zoomSettings.ZoomSensitivity;
        }

        public override void ExecuteFixedUpdate()
        {
            ProcessMoving();
            ProcessZooming();
            ProcessRecenter();
        }

        private void ProcessRecenter()
        {
            if (CanRecenter)
            {
                var followDirection = followTarget.forward.normalized;
                var targetDirection = transform.forward.normalized;
                var lerpDirection = Vector3.Slerp(followDirection, targetDirection, Time.deltaTime);
                followTarget.forward = lerpDirection;
            }
        }

        private void ProcessZooming()
        {
            _currentTargetDistance = Mathf.Clamp(
                _currentTargetDistance + _zoomDelta, 
                zoomSettings.MinDistance, 
                zoomSettings.MaxDistance);
            
            var smoothDistance = Mathf.Lerp(
                _bodyTransposer.CameraDistance, 
                _currentTargetDistance, 
                zoomSettings.Smoothing * Time.deltaTime);
            
            _bodyTransposer.CameraDistance = smoothDistance;

            var lerpTime = Mathf.InverseLerp(zoomSettings.MinDistance, zoomSettings.MaxDistance, smoothDistance);
            _bodyTransposer.CameraSide = Mathf.Lerp(1f, 0.5f, lerpTime);
        }

        private void ProcessMoving()
        {
            var delta = _mouseLook * _sensitivity * Time.deltaTime;
            followTarget.rotation *= Quaternion.AngleAxis(delta.x, Vector3.up);
            followTarget.rotation *= Quaternion.AngleAxis(delta.y, Vector3.right);

            var angles = followTarget.localEulerAngles;
            angles.z = 0;

            var angle = angles.x;

            if (angle > 180f && angle < moveSettings.MaxXAngle)
            {
                angles.x = moveSettings.MaxXAngle;
            }
            else if (angle < 180 && angle > moveSettings.MinXAngle)
            {
                angles.x = moveSettings.MinXAngle;
            }

            followTarget.localEulerAngles = angles;
        }

        private void GamepadActivated()
        {
            _sensitivity = moveSettings.GamepadSensitivity;
            characterInput.MoveAxis.performed += GamepadMovePerformed;
            characterInput.MoveAxis.canceled += GamepadMoveCanceled;
        }

        private void KeyboardActivated()
        {
            _sensitivity = moveSettings.MouseSensitivity;
            characterInput.MoveAxis.performed -= GamepadMovePerformed;
        }

        private void GamepadMovePerformed(InputAction.CallbackContext context)
        {
            CanRecenter = true;
        }

        private void GamepadMoveCanceled(InputAction.CallbackContext context)
        {
            CanRecenter = false;
        }
    }
}
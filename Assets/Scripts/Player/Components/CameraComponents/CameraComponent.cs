using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;
using UnityEngine.Serialization;

namespace Player.Components.CameraComponents
{
    public class CameraComponent : CharacterComponent
    {
        [SerializeField] private Transform followTarget;
        
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        
        [Header("Camera moving settings")]
        [SerializeField] private float moveSensitivity = 5f;
        [SerializeField] private float minXAngle = 40f;
        [SerializeField] private float maxXAngle = 340f;
        
        [Header("Camera zooming settings")] 
        [SerializeField] private float zoomSensitivity = 2f;
        [SerializeField] private float smoothing = 5f;
        [Space]
        [SerializeField] private float defaultDistance = 2f;
        [SerializeField] private float minDistance = 1f;
        [SerializeField] private float maxDistance = 3f;
        
        [Space(10)]
        [SerializeField] private string directionParameter = "Direction";

        private Cinemachine3rdPersonFollow _bodyTransposer;
                
        private Vector2 mouseLook;
        private float zoomDelta;
        private float _currentTargetDistance;
        
        private float xRotation;
        private float yRotation;

        private int directionKey;

        public override void SetupAction()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _currentTargetDistance = defaultDistance;
            _bodyTransposer = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            
            directionKey = UnityEngine.Animator.StringToHash(directionParameter);
        }

        public override void ExecuteUpdate()
        {
            mouseLook = _input.Look.ReadValue<Vector2>();
            zoomDelta = -_input.Zoom.ReadValue<float>() * zoomSensitivity;
        }

        public override void ExecuteFixedUpdate()
        {
            ProcessMoving();
            ProcessZooming();
        }

        private void ProcessZooming()
        {
            _currentTargetDistance = Mathf.Clamp(_currentTargetDistance + zoomDelta, minDistance, maxDistance);
            
            var smoothDistance = Mathf.Lerp(_bodyTransposer.CameraDistance, _currentTargetDistance, smoothing * Time.deltaTime);
            
            _bodyTransposer.CameraDistance = smoothDistance;

            var lerpTime = Mathf.InverseLerp(minDistance, maxDistance, smoothDistance);
            _bodyTransposer.CameraSide = Mathf.Lerp(1f, 0.5f, lerpTime);

        }

        private void ProcessMoving()
        {
            if (mouseLook == Vector2.zero)
            {
                Animator.SetFloatSmooth(directionKey, 0);
                return;
            }
            
            
            var delta = mouseLook * moveSensitivity * Time.deltaTime;
            followTarget.rotation *= Quaternion.AngleAxis(delta.x, Vector3.up);
            followTarget.rotation *= Quaternion.AngleAxis(delta.y, Vector3.right);

            var angles = followTarget.localEulerAngles;
            angles.z = 0;

            var angle = angles.x;

            if (angle > 180f && angle < maxXAngle)
            {
                angles.x = maxXAngle;
            }
            else if (angle < 180 && angle > minXAngle)
            {
                angles.x = minXAngle;
            }

            followTarget.localEulerAngles = angles;
        }
    }
}
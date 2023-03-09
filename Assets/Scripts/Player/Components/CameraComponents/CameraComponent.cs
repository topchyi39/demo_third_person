using Cinemachine;
using UnityEngine;

namespace Player.Components.CameraComponents
{
    public class CameraComponent : CharacterComponent
    {
        [SerializeField] private Transform followTarget;
        [Header("Camera moving settings")]
        [SerializeField] private float sensitivity;
        [SerializeField] private float minXAngle = 40f;
        [SerializeField] private float maxXAngle = 340f;
        
        private Vector2 mouseLook;
        
        private float xRotation;
        private float yRotation;
        
        public override void SetupAction()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override void ExecuteUpdate()
        {
            mouseLook = _input.MouseLook.ReadValue<Vector2>();
        }

        public override void ExecuteFixedUpdate()
        {
            var delta = mouseLook * sensitivity * Time.deltaTime;
            followTarget.rotation *= Quaternion.AngleAxis(delta.x, Vector3.up); 
            followTarget.rotation *= Quaternion.AngleAxis(delta.y, Vector3.right);
            
            var angles = followTarget.localEulerAngles;
            angles.z = 0;
            
            var angle = angles.x;
            
            if(angle  > 180f && angle < maxXAngle)
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
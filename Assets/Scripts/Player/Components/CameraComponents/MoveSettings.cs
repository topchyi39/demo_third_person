using System;
using UnityEngine;

namespace Player.Components.CameraComponents
{
    [Serializable]
    public class MoveSettings
    {
        [SerializeField] private float mouseSensitivity = 5f;
        [SerializeField] private float gamepadSensitivity = 50f;
        
        [SerializeField] private float minXAngle = 40f;
        [SerializeField] private float maxXAngle = 340f;

        public float MouseSensitivity => mouseSensitivity;
        public float GamepadSensitivity => gamepadSensitivity;
        public float MinXAngle => minXAngle;
        public float MaxXAngle => maxXAngle;
    }
}
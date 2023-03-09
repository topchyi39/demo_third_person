using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Input : MonoBehaviour
    {
        private PlayerInputActions _inputActions;


        public InputAction MoveAxis => _inputActions.Keyboard.MoveAxis;
        public InputAction MouseLook => _inputActions.Keyboard.MouseLook;
        
        private void OnEnable()
        {
            _inputActions ??= new PlayerInputActions();
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }
        
    }
}
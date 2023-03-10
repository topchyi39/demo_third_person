using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Input : MonoBehaviour
    {
        private PlayerInputActions _inputActions;


        public InputAction MoveAxis => _inputActions.Keyboard.MoveAxis;
        public InputAction Look => _inputActions.Keyboard.Look;
        public InputAction Zoom => _inputActions.Keyboard.Zoom;
        
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
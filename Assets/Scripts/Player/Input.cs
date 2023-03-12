using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    
    public class Input : MonoBehaviour
    {
        private PlayerInputActions _inputActions;


        public InputAction MoveAxis => _inputActions.Character.MoveAxis;
        public InputAction Look => _inputActions.Character.Look;
        public InputAction Zoom => _inputActions.Character.Zoom;
        public InputAction WalkToggle => _inputActions.Character.WalkToggle;

        private bool _gamepadActivated;
        
        public event Action KeyboardActivated;
        public event Action GamepadActivated;
        
        private void OnEnable()
        {
            _inputActions ??= new PlayerInputActions();
            _inputActions.Enable();
            
            _inputActions.Character.Keyboard.started += KeyboardStarted;
            _inputActions.Character.Gamepad.started += GamepadStarted;
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Character.Keyboard.started -= KeyboardStarted;
            _inputActions.Character.Gamepad.started -= GamepadStarted;
            
        }

        private void KeyboardStarted(InputAction.CallbackContext context)
        {
            if (!_gamepadActivated) return;

            _gamepadActivated = false;
            
            KeyboardActivated?.Invoke();
        }

        private void GamepadStarted(InputAction.CallbackContext context)
        {
            if (_gamepadActivated) return;

            _gamepadActivated = true;
            
            GamepadActivated?.Invoke();

        }
        
    }
}
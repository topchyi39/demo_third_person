using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    
    public class CharacterInput : MonoBehaviour
    {
        private GameInputActions _inputActions;


        public InputAction MoveAxis => _inputActions.Character.MoveAxis;
        public InputAction Look => _inputActions.Character.Look;
        public InputAction Zoom => _inputActions.Character.Zoom;
        public InputAction WalkToggle => _inputActions.Character.WalkToggle;
        public InputAction Dash => _inputActions.Character.Dash;
        public InputAction Jump => _inputActions.Character.Jump;
        public InputAction Roll => _inputActions.Character.Roll;
        public InputAction Crouch => _inputActions.Character.Crouch;

        public bool IsGamepadActivated { get; private set; }
        
        public event Action KeyboardActivated;
        public event Action GamepadActivated;
        
        private void OnEnable()
        {
            _inputActions ??= new GameInputActions();
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
            if (!IsGamepadActivated) return;

            IsGamepadActivated = false;
            
            KeyboardActivated?.Invoke();
        }

        private void GamepadStarted(InputAction.CallbackContext context)
        {
            if (IsGamepadActivated) return;

            IsGamepadActivated = true;
            
            GamepadActivated?.Invoke();

        }
        
    }
}
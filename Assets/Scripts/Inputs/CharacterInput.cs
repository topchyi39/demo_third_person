using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Inputs
{
    public interface ICharacterInput
    {
        InputAction MoveAxis { get; }
        InputAction Look { get; }
        InputAction Zoom { get; }
        InputAction WalkToggle { get; }
        InputAction Dash { get; }
        InputAction Jump { get; }
        InputAction Roll { get; }
        InputAction Crouch { get; }

        bool IsGamepadActivated { get; }
        
        event Action KeyboardActivated;
        event Action GamepadActivated;
    }
    
    
    public class CharacterInput : MonoBehaviour, ICharacterInput
    {
        private IInput _input;
        
        public InputAction MoveAxis => _input.CharacterActions.MoveAxis;
        public InputAction Look => _input.CharacterActions.Look;
        public InputAction Zoom => _input.CharacterActions.Zoom;
        public InputAction WalkToggle => _input.CharacterActions.WalkToggle;
        public InputAction Dash => _input.CharacterActions.Dash;
        public InputAction Jump => _input.CharacterActions.Jump;
        public InputAction Roll => _input.CharacterActions.Roll;
        public InputAction Crouch => _input.CharacterActions.Crouch;

        public bool IsGamepadActivated { get; private set; }
        
        public event Action KeyboardActivated;
        public event Action GamepadActivated;

        [Inject]
        private void Construct(IInput input)
        {
            _input = input;
        }
        
        private void OnEnable()
        {
            _input.CharacterActions.Keyboard.started += KeyboardStarted;
            _input.CharacterActions.Gamepad.started += GamepadStarted;
        }

        private void OnDisable()
        {
            _input.CharacterActions.Keyboard.started -= KeyboardStarted;
            _input.CharacterActions.Gamepad.started -= GamepadStarted;
            
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
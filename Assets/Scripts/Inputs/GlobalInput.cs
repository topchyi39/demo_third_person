using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class GlobalInput : MonoBehaviour
    {
        private GameInputActions _inputActions;

        public InputAction Interact => _inputActions.Global.Interact;

        private void OnEnable()
        {
            _inputActions ??= new GameInputActions();
            _inputActions.Enable();

        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }
    }
}
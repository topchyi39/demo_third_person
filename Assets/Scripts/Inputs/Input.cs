using UnityEngine;

namespace Inputs
{
    public class Input : MonoBehaviour
    {
        private GameInputActions _inputActions;

        public GameInputActions.CharacterActions CharacterActions => _inputActions.Character;
        public GameInputActions.GlobalActions GlobalActions => _inputActions.Global;

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
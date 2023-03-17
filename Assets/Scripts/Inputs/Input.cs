using UnityEngine;

namespace Inputs
{
    public interface IInput
    {
        public GameInputActions.CharacterActions CharacterActions { get; }
        public GameInputActions.InGameActions InGameActions { get; }
    }
    
    public class Input : MonoBehaviour, IInput
    {
        private GameInputActions _inputActions;

        public GameInputActions.CharacterActions CharacterActions => _inputActions.Character;
        public GameInputActions.InGameActions InGameActions => _inputActions.InGame;

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
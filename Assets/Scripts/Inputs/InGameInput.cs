using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Inputs
{
    public interface IInGameInput
    {
        InputAction Interact { get; }
        InputAction Escape { get; }
        InputAction Inventory { get; }

        void Enable();
        void Disable();
    }
    
    public class InGameInput : MonoBehaviour, IInGameInput
    {
        private IInput _input;

        public InputAction Interact => _input.InGameActions.Interact;
        public InputAction Escape => _input.InGameActions.Escape;
        public InputAction Inventory => _input.InGameActions.Inventory;

        [Inject]
        private void Construct(IInput input)
        {
            _input = input;
        }
        
        public void Enable()
        {
            _input.InGameActions.Enable();
        }

        public void Disable()
        {
            _input.InGameActions.Disable();
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Inputs
{
    public interface IInGameInput
    {
        InputAction Interact { get; }
    }
    
    public class InGameInput : MonoBehaviour, IInGameInput
    {
        private IInput _input;

        public InputAction Interact => _input.InGameActions.Interact;
        
        [Inject]
        private void Construct(IInput input)
        {
            _input = input;
        }

    }
}
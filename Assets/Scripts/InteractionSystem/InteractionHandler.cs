using Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace InteractionSystem
{
    public class InteractionHandler : MonoBehaviour
    {
        private IInGameInput _inGameInput;
        
        [Inject]
        private void Construct(IInGameInput inGameInput)
        {
            _inGameInput = inGameInput;
        }

        private void Start()
        {
            _inGameInput.Interact.performed += InteractPerformed;
        }
        
        private void OnDestroy()
        {
            _inGameInput.Interact.performed -= InteractPerformed;
        }

        private void InteractPerformed(InputAction.CallbackContext context)
        {
            Debug.LogError("Interact performed");
        }
    }
}
using Inputs;
using UI.Screens.Menu;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace UI.UIControls
{
    public class MenuControl : UIControl
    {
        private IInGameInput _inGameInput;
        private UIManager _uiManager;
        
        [Inject]
        private void Construct(IInGameInput inGameInput, UIManager uiManager)
        {
            _inGameInput = inGameInput;
            _uiManager = uiManager;
        }

        public override void Enable()
        {
            _inGameInput.Escape.performed += EscapePerformed;
        }

        public override void Disable()
        {
            _inGameInput.Escape.performed -= EscapePerformed;
        }

        private void EscapePerformed(InputAction.CallbackContext context)
        {
            _uiManager.OpenPrevious();
        }
    }
}
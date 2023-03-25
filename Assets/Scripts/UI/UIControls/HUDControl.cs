using System;
using Inputs;
using UI.Screens.Menu;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace UI.UIControls
{
    public class HUDControl : UIControl
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
            _inGameInput.Inventory.performed += InventoryPerformed;
        }

        public override void Disable()
        {
            _inGameInput.Escape.performed -= EscapePerformed;
            _inGameInput.Inventory.performed -= InventoryPerformed;
        }

        private void EscapePerformed(InputAction.CallbackContext context)
        {
            _uiManager.OpenScreenWithParams<Menu>(MenuParams.DefaultParams);
        }

        private void InventoryPerformed(InputAction.CallbackContext context)
        {
            _uiManager.OpenScreenWithParams<Menu>(new MenuParams{ tabTypeOnShow = MenuTabType.Inventory});

        }
    }
}
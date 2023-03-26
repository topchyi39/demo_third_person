using System;
using DefaultNamespace.InteractionSystem.Subjects;
using Inputs;
using UI;
using UI.Screens.HUDs;
using UI.Screens.HUDs.HUDWidgets.Interact;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace InteractionSystem
{
    public class InteractionHandler : MonoBehaviour
    {
        private IInGameInput _inGameInput;
        private UIManager _uiManager;

        private IInteractionSubject _lastSubject;
        
        private HUD _hud;
        
        private Action _hideAction;
        
        [Inject]
        private void Construct(IInGameInput inGameInput, UIManager uiManager)
        {
            _inGameInput = inGameInput;
            _uiManager = uiManager;
        }

        private void Start()
        {
            _hud = _uiManager.GetScreen<HUD>();
        }

        private void OnEnable()
        {
            _inGameInput.Interact.performed += InteractPerformed;
        }
        
        private void OnDisable()
        {
            _inGameInput.Interact.performed -= InteractPerformed;
        }

        private void InteractPerformed(InputAction.CallbackContext context)
        {
            
            if(_lastSubject == null) return;
            
            _lastSubject.Interact();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_lastSubject != null) return;
            
            if (!other.TryGetComponent<IInteractionSubject>(out var subject)) return;

            _lastSubject = subject;
            _hud.ShowWidgetWithParams<InteractWidget>(new InteractParams {interactText = "Interact"});
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<IInteractionSubject>(out var subject)) return;
            
            if(_lastSubject != subject) return;
            
            _hud.HideWidget<InteractWidget>();
            _lastSubject = null;
            
        }
    }
}
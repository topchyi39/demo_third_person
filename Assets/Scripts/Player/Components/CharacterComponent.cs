using Inputs;
using UnityEngine;

namespace Player.Components
{
    public abstract class CharacterComponent : MonoBehaviour
    {
        [SerializeField] private int priority;

        protected bool _blockOther;
        protected CharacterController _characterController;
        protected CharacterInput characterInput;
        
        public int Priority => priority;
        public CharacterInput CharacterInput => characterInput;
        public CharacterAnimator Animator => _characterController.CharacterAnimator;
        
        public void Setup(CharacterController characterController)
        {
            _characterController = characterController;
            characterInput = _characterController.CharacterInput;
            SetupAction();
        }

        public abstract void SetupAction();

        public abstract void ExecuteUpdate();

        public abstract void ExecuteFixedUpdate();
    
    }
}
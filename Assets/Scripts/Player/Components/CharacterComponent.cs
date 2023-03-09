﻿using UnityEngine;

namespace Player.Components
{
    public abstract class CharacterComponent : MonoBehaviour
    {
        [SerializeField] private int priority;

        protected bool _blockOther;
        protected CharacterController _characterController;
        protected Input _input;
        
        public int Priority => priority;
        public bool BlockOther => _blockOther;

        public void Setup(CharacterController characterController)
        {
            _characterController = characterController;
            _input = _characterController.Input;
            SetupAction();
        }

        public abstract void SetupAction();

        public abstract void ExecuteUpdate();

        public abstract void ExecuteFixedUpdate();
    
    }
}
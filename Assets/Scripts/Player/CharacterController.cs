using System;
using System.Collections.Generic;
using System.Linq;
using Inputs;
using Player.Components;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(CharacterInput))]
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator characterAnimator;
        [FormerlySerializedAs("input")] [SerializeField] private CharacterInput characterInput;
        
        private IEnumerable<CharacterComponent> _components;
        private CharacterComponent[] _componentsArray;

        public CharacterAnimator CharacterAnimator => characterAnimator;
        public CharacterInput CharacterInput => characterInput;

        private void Awake()
        {
            FindCharacterComponents();
        }

        private void Start()
        {
            foreach (var characterComponent in _components)
            {
                characterComponent.Setup(this);
            }
        }

        /// <summary>
        /// Find character components from this object
        /// </summary>
        private void FindCharacterComponents()
        {
            var components = GetComponents<CharacterComponent>();

            _components = components.OrderBy(component => component.Priority);
            _componentsArray = _components.ToArray();

            
        }

        /// <summary>
        /// Execute character components update method 
        /// </summary>
        private void Update()
        {
            foreach (var characterComponent in _components)
            {
                characterComponent.ExecuteUpdate();
            }
        }

        /// <summary>
        /// Execute character components fixed update method 
        /// </summary>
        private void FixedUpdate()
        {
            foreach (var characterComponent in _components)
            {
                characterComponent.ExecuteFixedUpdate();
            }
        }

        public T GetCharacterComponent<T>() where T : CharacterComponent
        {
            return (T) _components.First(item => item is T);
        }
    }
}
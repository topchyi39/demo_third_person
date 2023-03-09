using System.Collections.Generic;
using System.Linq;
using Player.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Input))]
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Input input; 
        
        private IEnumerable<CharacterComponent> _components;
        private CharacterComponent[] _componentsArray;

        public Input Input => input;
        
        private void Awake()
        {
            FindCharacterComponents();
        }

        /// <summary>
        /// Find character components from this object
        /// </summary>
        private void FindCharacterComponents()
        {
            var components = GetComponents<CharacterComponent>();

            _components = components.OrderBy(component => component.Priority);
            _componentsArray = _components.ToArray();

            foreach (var characterComponent in _components)
            {
                characterComponent.Setup(this);
            }
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
    }
}
using System;
using UnityEngine;

namespace Player.Components.MovementComponents.Utility.Collider
{
    [Serializable]
    public class TriggerColliderData
    {
        [SerializeField] private BoxCollider groundCheckCollider;

        public BoxCollider GroundCheckCollider => groundCheckCollider;
        public Vector3 Extends { get; private set; }
        
        public void Initialize()
        {
            Extends = groundCheckCollider.bounds.extents;
        }
    }
    
    public class CharacterResizableCapsuleCollider : ResizableCapsuleCollider
    {
        [SerializeField] private TriggerColliderData colliderData;

        public TriggerColliderData ColliderData => colliderData;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            colliderData.Initialize();
        }
    }
}
using System;
using UnityEngine;

namespace Player.Utility.Collider
{
    [Serializable]
    public class PlayerTriggerColliderData
    {
        [SerializeField] private BoxCollider groundCheckCollider;
        public BoxCollider GroundCheckCollider => groundCheckCollider;

        public Vector3 GroundCheckColliderVerticalExtents { get; private set; }

        public void Initialize()
        {
            if(groundCheckCollider)
                GroundCheckColliderVerticalExtents = GroundCheckCollider.bounds.extents;
        }
    }
}
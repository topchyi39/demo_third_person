using System;
using UnityEngine;

namespace Player.Components.MovementComponents.Utility.Collider
{
    [Serializable]
    public class DefaultColliderData
    {
        [field: Tooltip("The height is known through the Model Mesh Renderer \"bounds.size\" variable.")]
        [SerializeField] private float height = 1.8f;
        [SerializeField] private float centerY = 0.9f;
        [SerializeField] private float radius = 0.2f;
        
        public float Height => height;
        public float CenterY => centerY;
        public float Radius => radius;
    }
}
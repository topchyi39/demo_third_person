using System;
using UnityEngine;

namespace Player.Utility.Collider
{
    [Serializable]
    public class DefaultColliderData
    {
        [SerializeField] private float height = 1.8f;
        [SerializeField] private float centerY = 0.9f;
        [SerializeField] private float radius = 0.2f;

        public float Height => height;
        public float CenterY => centerY;
        public float Radius => radius;
    }
}
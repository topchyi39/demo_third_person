using System;
using UnityEngine;

namespace Player.Components.MovementComponents.Utility.Collider
{
    [Serializable]
    public class CapsuleSettings
    {
        [SerializeField] private float height;
        [SerializeField] private float center;

        public float Height => height;
        public float Center => center;
    }
}
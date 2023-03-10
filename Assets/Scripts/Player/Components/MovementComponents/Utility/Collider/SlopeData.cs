using System;
using UnityEngine;

namespace Player.Components.MovementComponents.Utility.Collider
{
    [Serializable]
    public class SlopeData
    {
        [SerializeField] [Range(0f, 1f)] private float stepHeightPercentage = 0.25f;
        [SerializeField] [Range(0f, 5f)] private float floatRayDistance = 2f;
        [SerializeField] [Range(0f, 50f)] private float stepReachForce = 25f;
        
        public float StepHeightPercentage => stepHeightPercentage;
        public float FloatRayDistance => floatRayDistance;
        public float StepReachForce => stepReachForce;
    }
}
using System;
using UnityEngine;

namespace Player.Components.MovementComponents.Data
{
    [Serializable]
    public class MovementStateData
    {
        [SerializeField] private float speedModifier;
        [SerializeField] private AnimationCurve modifierCurve;
        [SerializeField] private float decelerationForce;
        [SerializeField] private Vector3 timeToReachRotation;
        
        public float SpeedModifier => speedModifier;
        public AnimationCurve ModifierCurve => modifierCurve;
        public float DecelerationForce => decelerationForce;
        public Vector3 TimeToReachRotation => timeToReachRotation;
    }
}
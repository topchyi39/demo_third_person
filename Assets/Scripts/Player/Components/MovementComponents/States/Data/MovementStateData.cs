using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Components.MovementComponents.States.Data
{
    [Serializable]
    public class MovementStateData
    {
        [SerializeField] private float speedModifier;
        [SerializeField] private float delayBeforeStart;
        [SerializeField] private float startSpeedModifier;
        [SerializeField] private float decelerationForce;
        [SerializeField] private Vector3 timeToReachRotation;
        
        public float SpeedModifier => speedModifier;
        public float StartSpeedModifier => startSpeedModifier;
        public float DecelerationForce => decelerationForce;
        public Vector3 TimeToReachRotation => timeToReachRotation;
    }
}
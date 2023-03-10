using System;
using UnityEngine;

namespace Player.Components.MovementComponents.States.Data
{
    [Serializable]
    public class MovementStateData
    {
        [SerializeField] private float speedModifier;
        [SerializeField] private Vector3 timeToReachRotation;
        
        public float SpeedModifier => speedModifier;
        public Vector3 TimeToReachRotation => timeToReachRotation;
    }
}
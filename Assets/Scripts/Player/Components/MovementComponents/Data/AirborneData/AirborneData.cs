using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Components.MovementComponents.Data.AirborneData
{
    [Serializable]
    public class FallData
    {
        [SerializeField, Range(0, 10)] private float fallSpeedLimit;
        [SerializeField, Range(0, 100)] private float minimumDistanceToHardFall;

        public float FallSpeedLimit => fallSpeedLimit;
        public float MinimumDistanceToHardFall => minimumDistanceToHardFall;
    }

    [Serializable]
    public class JumpData
    {
        [SerializeField] private float jumpToGroundRayDistance;
        [SerializeField] private Vector3 timeToReachRotation;

        [SerializeField] private AnimationCurve onSlopeUpwards;
        [SerializeField] private AnimationCurve onSlopeDownwards;
        
        
        [SerializeField] private Vector3 stationaryForce;
        [SerializeField] private Vector3 weakForce;
        [SerializeField] private Vector3 mediumForce;
        [SerializeField] private Vector3 strongForce;
        [SerializeField] private float decelerationForce;

        public float JumpToGroundRayDistance => jumpToGroundRayDistance;
        public Vector3 TimeToReachRotation => timeToReachRotation;
        public AnimationCurve OnSlopeUpwards => onSlopeUpwards;
        public AnimationCurve OnSlopeDownwards => onSlopeDownwards;
        public Vector3 StationaryForce => stationaryForce;
        public Vector3 WeakForce => weakForce;
        public Vector3 MediumForce => mediumForce;
        public Vector3 StrongForce => strongForce;
        public float DecelerationForce => decelerationForce;
    }
    
    [Serializable]
    public class AirborneData
    {
        [SerializeField] private FallData fallData;
        [SerializeField] private JumpData jumpData;

        public FallData FallData => fallData;
        public JumpData JumpData => jumpData;
    }
}
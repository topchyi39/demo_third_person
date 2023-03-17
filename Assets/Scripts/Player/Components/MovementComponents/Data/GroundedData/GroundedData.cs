using System;
using Player.Components.MovementComponents.Utility.Collider;
using UnityEngine;

namespace Player.Components.MovementComponents.Data.GroundedData
{
    [Serializable]
    public class GroundedData
    {
        [SerializeField] private float distanceToFall;
        
        [SerializeField] private CapsuleSettings defaultCapsuleSettings;
        [SerializeField] private CapsuleSettings crouchCapsuleSettings;
        [SerializeField] private MovementStateData walkData;
        [SerializeField] private MovementStateData jogData;
        [SerializeField] private MovementStateData dashData;
        [SerializeField] private MovementStateData rollData;

        public float DistanceToFall => distanceToFall;
        public CapsuleSettings DefaultCapsuleSettings => defaultCapsuleSettings;
        public CapsuleSettings CrouchCapsuleSettings => crouchCapsuleSettings;
        public MovementStateData WalkData => walkData;
        public MovementStateData JogData => jogData;
        public MovementStateData DashData => dashData;
        public MovementStateData RollData => rollData;
    }
}
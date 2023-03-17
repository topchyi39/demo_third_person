using UnityEngine;

namespace Player.Components.MovementComponents.Data
{
    public class ReusableData
    {
        //Input data
        public Vector2 MoveAxis { get; set; }
        public Vector2 LastMoveAxis { get; set; }
        public Vector2 LookDelta { get; set; }
        public bool CanUpdateMoveAnimation { get; set; }
        
        //Move Data
        public float SpeedModifier { get; set; }
        public float SlopeSpeedModifier { get; set; } = 1f;
        public bool ShouldWalk { get; set; }
        public bool ShouldDash { get; set; }
        public bool ShouldCrouch { get; set; }
        
        //Rotate Data
        private Vector3 _targetRotation;
        private Vector3 _dampedTargetRotation;
        private Vector3 _timeToReachRotation;
        
        //Jump Data
        private Vector3 _jumpForce;
        
        public ref Vector3 TargetRotation => ref _targetRotation;
        public ref Vector3 DampedTargetRotation => ref _dampedTargetRotation;
        public ref Vector3 TimeToReachRotation => ref _timeToReachRotation;
        public ref Vector3 JumpForce => ref _jumpForce;
        
        public float DampedTargetRotationPassedTime { get; set; }
        public float MovementDecelerationForce { get; set; }
    }
}
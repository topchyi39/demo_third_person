using UnityEngine;

namespace Player.Components.MovementComponents.States.Data
{
    public class ReusableData
    {
        public Vector2 MoveAxis { get; set; }
        public Vector2 LookDelta { get; set; }
        
        //Move Data
        public float SpeedModifier { get; set; }
        public bool ShouldWalk { get; set; }
        
        //Rotate Data
        private Vector3 _targetRotation;
        private Vector3 _dampedTargetRotation;
        private Vector3 _timeToReachRotation;
        
        public ref Vector3 TargetRotation => ref _targetRotation;
        public ref Vector3 DampedTargetRotation => ref _dampedTargetRotation;
        public ref Vector3 TimeToReachRotation => ref _timeToReachRotation;
        
        public float DampedTargetRotationPassedTime { get; set; }
        public float MovementDecelerationForce { get; set; }
    }
}
using UnityEngine;

namespace Player.Components.MovementComponents.States.Data
{
    public class ReusableData
    {
        public Vector2 MoveAxis { get; set; }
        
        //Move Data
        public float SpeedModifier { get; set; }
        
        //Rotate Data
        private Vector3 _targetRotation;
        private Vector3 _dampedTargetRotation;
        private Vector3 _timeToReachTargetRotation;
        
        public ref Vector3 TargetRotation => ref _targetRotation;
        public ref Vector3 DampedTargetRotation => ref _dampedTargetRotation;
        public ref Vector3 TimeToReachTargetRotation => ref _timeToReachTargetRotation;
        
        public float SmoothTime => _timeToReachTargetRotation.y - DampedTargetRotationPassedTime;
        
        public float DampedTargetRotationPassedTime { get; set; }
    }
}
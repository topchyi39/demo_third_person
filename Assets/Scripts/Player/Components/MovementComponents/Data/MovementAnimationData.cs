using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Components.MovementComponents.Data
{
    [Serializable]
    public class AnimParameters
    {
        [SerializeField] private string startParameter;
        [SerializeField] private string continuousParameter;
        [SerializeField] private string endParameter;

        private int? _startKey;
        private int? _continuousKey;
        private int? _endKey;
       
        public int StartKey => _startKey ??= Animator.StringToHash(startParameter);
        public int ContinuousKey => _continuousKey ??= Animator.StringToHash(continuousParameter);
        public int EndKey => _endKey ??= Animator.StringToHash(endParameter);

        
        public AnimParameters(string startParameter, string continuousParameter, string endParameter)
        {
            this.startParameter = startParameter;
            this.continuousParameter = continuousParameter;
            this.endParameter = endParameter;
        }
    }
    
    [Serializable]
    public class MovementAnimationData
    {
        [SerializeField] private string verticalAxisParameter = "VerticalAxis";
        [SerializeField] private string horizontalAxisParameter = "HorizontalAxis";
        [SerializeField] private string turnParameter = "Turn";
        
        [Header("Grounded Parameters")]
        [SerializeField] private string groundedParameter = "Grounded";
        [SerializeField] private string idleParameter = "Idle";
        
        [SerializeField] private string movingParameter = "Moving";
        
        [SerializeField] private string startingParameter = "StartMoving";
        [SerializeField] private string stoppingParameter = "StopMoving";
        
        [SerializeField] private string walkParameter = "Walk";
        [SerializeField] private string jogParameter = "Jog";
        [SerializeField] private string dashParameter = "Dash";

        [SerializeField] private string rollParameter = "Roll";
        [SerializeField] private string crouchParameter = "Crouch";
        
        [Header("Grounded Landing Parameter")] 
        [SerializeField] private string landingParameter = "Landing";

        [SerializeField] private string lightLandingParameter = "Light";
        [SerializeField] private string hardLandingParameter = "Hard";
        
        [Header("Airborne Parameters")]
        [SerializeField] private string airborneParameter = "Airborne";
        [SerializeField] private string fallParameter = "Fall";
        [SerializeField] private string jumpParameter = "Jump";
        
        private int? _verticalAxisKey;
        private int? _horizontalAxisKey;
        private int? _turnKey;
        
        //grounded keys
        private int? _groundedKey;
        
        private int? _idleKey;
        private int? _movingKey;
        
        private int? _startingKey;
        private int? _stoppingKey;
        
        private int? _walkKey;
        private int? _jogKey;
        private int? _dashKey;

        private int? _rollKey;
        private int? _crouchKey;
        
        private int? _landingKey;
        private int? _lightLandingKey;
        private int? _hardLandingKey;
        
        //airborne keys
        private int? _airborneKey;
        private int? _fallKey;
        private int? _jumpKey;

        public int VerticalKey => _verticalAxisKey ??= Animator.StringToHash(verticalAxisParameter);
        public int HorizontalKey => _horizontalAxisKey ??= Animator.StringToHash(horizontalAxisParameter);
        public int TurnKey => _turnKey ??= Animator.StringToHash(turnParameter);
        
        public int GroundedKey => _groundedKey ??= Animator.StringToHash(groundedParameter);
        public int IdleKey => _idleKey ??= Animator.StringToHash(idleParameter);
        public int MovingKey => _movingKey ??= Animator.StringToHash(movingParameter);
        public int StartingKey => _startingKey ??= Animator.StringToHash(startingParameter);
        public int StoppingKey => _stoppingKey ??= Animator.StringToHash(stoppingParameter);
        public int WalkKey => _walkKey ??= Animator.StringToHash(walkParameter);
        public int JogKey => _jogKey ??= Animator.StringToHash(jogParameter);
        public int DashKey => _dashKey ??= Animator.StringToHash(dashParameter);
        public int RollKey => _rollKey ??= Animator.StringToHash(rollParameter);
        public int CrouchKey => _crouchKey ??= Animator.StringToHash(crouchParameter);
        
        public int LandingKey => _landingKey ??= Animator.StringToHash(landingParameter);
        public int LightLandingKey => _lightLandingKey ??= Animator.StringToHash(lightLandingParameter);
        public int HardLandingKey => _hardLandingKey ??= Animator.StringToHash(hardLandingParameter);
        
        public int AirborneKey => _airborneKey ??= Animator.StringToHash(airborneParameter);
        public int FallKey => _fallKey ??= Animator.StringToHash(fallParameter);
        public int JumpKey => _jumpKey ??= Animator.StringToHash(jumpParameter);
        
    }
}
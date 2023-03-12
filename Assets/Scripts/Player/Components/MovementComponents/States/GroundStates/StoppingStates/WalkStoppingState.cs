using UnityEngine;

namespace Player.Components.MovementComponents.States.GroundStates.StoppingStates
{
    public class WalkStoppingState : StoppingState
    {
        protected override int moveKey => _animationData.WalkKey;
        
        public WalkStoppingState(MovementComponent component) : base(component) { }

        public override void Enter()
        {
            base.Enter();

            _reusableData.TimeToReachRotation = _movementData.WalkData.TimeToReachRotation;
            _reusableData.MovementDecelerationForce = _movementData.WalkData.DecelerationForce;
        }
    }
}
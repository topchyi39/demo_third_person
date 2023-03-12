using Player.Components.MovementComponents.States.GroundStates.MovingStates;
using UnityEngine;

namespace Player.Components.MovementComponents.States.GroundStates.StartingStates
{
    public class WalkStartingState : StartingState
    {
        protected override int moveKey => _animationData.WalkKey;
        protected override float speedModifier => _movementData.WalkData.SpeedModifier;
        protected override AnimationCurve modifierCurve => _movementData.WalkData.ModifierCurve;
        protected override MovingState nextState =>_component.StateMachine.WalkState;

        public WalkStartingState(MovementComponent component) : base(component)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _reusableData.TimeToReachRotation = _movementData.WalkData.TimeToReachRotation;
        }
    }
}
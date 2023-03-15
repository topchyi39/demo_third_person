using Player.Components.MovementComponents.States.GroundStates.MovingStates;
using UnityEngine;

namespace Player.Components.MovementComponents.States.GroundStates.StartingStates
{
    public class JogStartingState : StartingState
    {
        protected override int moveKey => _animationData.JogKey;
        protected override float speedModifier => _groundedData.JogData.SpeedModifier;
        protected override AnimationCurve modifierCurve => _groundedData.JogData.ModifierCurve;
        protected override MovingState nextState =>_component.StateMachine.JogState;
        
        public JogStartingState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _reusableData.TimeToReachRotation = _groundedData.JogData.TimeToReachRotation;
        }
    }
}
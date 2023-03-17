using Player.Components.MovementComponents.States.GroundStates.MovingStates;
using UnityEngine;

namespace Player.Components.MovementComponents.States.GroundStates.StartingStates
{
    public class DashStartingState : StartingState
    {
        protected override int moveKey => _animationData.DashKey;
        protected override float speedModifier => _groundedData.DashData.SpeedModifier;
        protected override AnimationCurve modifierCurve => _groundedData.DashData.ModifierCurve;
        protected override MovingState nextState =>_component.StateMachine.DashState;        
        
        public DashStartingState(MovementComponent component) : base(component)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            _reusableData.TimeToReachRotation = _groundedData.DashData.TimeToReachRotation;
        }
    }
}
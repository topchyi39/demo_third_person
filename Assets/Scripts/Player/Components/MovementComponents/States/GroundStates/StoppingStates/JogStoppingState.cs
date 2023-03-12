namespace Player.Components.MovementComponents.States.GroundStates.StoppingStates
{
    public class JogStoppingState : StoppingState
    {
        protected override int moveKey => _animationData.JogKey;

        public JogStoppingState(MovementComponent component) : base(component) { }

        public override void Enter()
        {
            base.Enter();
            
            _reusableData.TimeToReachRotation = _movementData.JogData.TimeToReachRotation;
            _reusableData.MovementDecelerationForce = _movementData.JogData.DecelerationForce;
        }
    }
}
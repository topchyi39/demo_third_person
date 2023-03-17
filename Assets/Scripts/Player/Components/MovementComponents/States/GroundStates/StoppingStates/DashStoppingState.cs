namespace Player.Components.MovementComponents.States.GroundStates.StoppingStates
{
    public class DashStoppingState : StoppingState
    {
        protected override int moveKey => _animationData.DashKey;
        
        public DashStoppingState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            _reusableData.TimeToReachRotation = _groundedData.DashData.TimeToReachRotation;
            _reusableData.MovementDecelerationForce = _groundedData.DashData.DecelerationForce;
        }
    }
}
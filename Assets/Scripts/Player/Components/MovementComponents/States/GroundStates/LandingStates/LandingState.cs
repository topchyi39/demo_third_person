namespace Player.Components.MovementComponents.States.GroundStates.LandingStates
{
    public class LandingState : GroundState
    {
        public LandingState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _reusableData.SpeedModifier = 0f;
        }

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.LandingKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.LandingKey, false);
        }

        #endregion
    }
}
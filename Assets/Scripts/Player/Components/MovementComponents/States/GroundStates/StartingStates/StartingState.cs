namespace Player.Components.MovementComponents.States.GroundStates.StartingStates
{
    public class StartingState : GroundState
    {
        public StartingState(MovementComponent component) : base(component)
        {
        }

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.StartingKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.StartingKey, false);

        }

        #endregion
    }
}
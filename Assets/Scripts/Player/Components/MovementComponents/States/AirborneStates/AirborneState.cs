namespace Player.Components.MovementComponents.States.AirborneStates
{
    public class AirborneState : BaseMoveState
    {
        public AirborneState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();
            ResetDash();
        }

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.AirborneKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            _component.Animator.SetBool(_animationData.AirborneKey, false);
        }

        #endregion
    }
}
using Player.Components.MovementComponents.States.GroundStates.LandingStates;

namespace Player.Components.MovementComponents.States.GroundStates.CrouchingStates
{
    public class CrouchLandState : LightLandState
    {
        
        public CrouchLandState(MovementComponent component) : base(component)
        {
        }
        
        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.CrouchKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.CrouchKey, false);
        }

        #endregion
    }
}
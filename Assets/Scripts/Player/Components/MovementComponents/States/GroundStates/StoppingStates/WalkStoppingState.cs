using UnityEngine;

namespace Player.Components.MovementComponents.States.GroundStates.StoppingStates
{
    public class WalkStoppingState : StoppingState
    {
        public WalkStoppingState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _reusableData.TimeToReachRotation = _movementData.WalkData.TimeToReachRotation;
            _reusableData.MovementDecelerationForce = _movementData.WalkData.DecelerationForce;
        }

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            if(_reusableData.MoveAxis == Vector2.zero)
                _component.Animator.SetBool(_animationData.WalkKey, true);
            
            base.StartStateAnimation();
        }

        protected override void EndStateAnimation()
        {
            _component.Animator.SetBool(_animationData.WalkKey, false);
            
            base.EndStateAnimation();
        }

        #endregion
    }
}
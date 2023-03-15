using UnityEngine;

namespace Player.Components.MovementComponents.States.AirborneStates
{
    public class FallState : AirborneState
    {
        private Vector3 _positionOnEnter;
        
        public FallState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            _reusableData.SpeedModifier = 0f;
            _positionOnEnter = _component.Rigidbody.position;
            ResetVerticalVelocity();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            LimitVerticalVelocity();
        }

        #region Physic Methods

        protected override void OnGroundEnter(Collider collider)
        {
            var fallDistance = _positionOnEnter.y - _component.Rigidbody.position.y;

            BaseMoveState state;

            if (fallDistance < _airborneData.FallData.MinimumDistanceToHardFall)
            {
                state = _reusableData.ShouldCrouch ? _component.StateMachine.CrouchLandState : _component.StateMachine.LightLandState;
            }
            else 
                state = _component.StateMachine.HardLandState;

            if (_reusableData.MoveAxis != Vector2.zero)
                state = _component.StateMachine.RollingState;
            
            _component.StateMachine.ChangeState(state);
        }

        private void LimitVerticalVelocity()
        {
            Vector3 playerVerticalVelocity = GetVerticalVelocity();

            if (playerVerticalVelocity.y >= -_airborneData.FallData.FallSpeedLimit)
            {
                return;
            }

            var limitedVelocityForce = new Vector3(0f, -_airborneData.FallData.FallSpeedLimit - playerVerticalVelocity.y, 0f);

            _component.Rigidbody.AddForce(limitedVelocityForce, ForceMode.VelocityChange);
        }

        #endregion


        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.FallKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            _component.Animator.SetBool(_animationData.FallKey, false);
        }

        #endregion
    }
}
using UnityEngine;

namespace Player.Components.MovementComponents.States.GroundStates
{
    public class GroundState : BaseMoveState
    {
        public GroundState(MovementComponent component) : base(component)
        {
            
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            Float();
        }

        private void Float()
        {
            var origin = _component.ResizableCollider.WorldCapsuleCentre;
            var direction = Vector3.down;
            var rayDistance = _component.ResizableCollider.SlopeData.FloatRayDistance;
            if (Physics.Raycast(origin, direction, out var hit, rayDistance, _component.LayerData.GroundLayer,
                    QueryTriggerInteraction.Ignore))
            {
                var groundAngle = Vector3.Angle(hit.normal, -direction);

                var distanceToFloatPoint =
                    _component.ResizableCollider.LocalCapsuleCentre.y * _component.transform.localScale.y -
                    hit.distance;
                
                
                if (distanceToFloatPoint == 0f) return;
                
                float amountToLift = distanceToFloatPoint * _component.ResizableCollider.SlopeData.StepReachForce - GetVerticalVelocity().y;
                Vector3 liftForce = new Vector3(0f, amountToLift, 0f);
                _component.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.GroundedKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.GroundedKey, false);
        }

        #endregion
    }
}
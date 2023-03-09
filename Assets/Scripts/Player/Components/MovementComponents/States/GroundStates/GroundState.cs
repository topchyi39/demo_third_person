using UnityEngine;

namespace Player.Components.MovementComponents.States.GroundStates
{
    public class GroundState : BaseMoveState
    {
        public GroundState(MovementComponent component) : base(component)
        {
            
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            Float();
        }

        private void Float()
        {
            var origin = _resizableCapsuleCollider.WorldCenter;
            var direction = Vector3.down;
            var rayDistance = _resizableCapsuleCollider.SlopeData.FloatRayDistance;
            var layer = _component.LayerData.GroundLayer;
            
            if (Physics.Raycast(origin, direction, out var hit, rayDistance, layer))
            {
                var groundAngle = Vector3.Angle(hit.normal, -direction);
                
                
                var localCapsuleCentre = _resizableCapsuleCollider.LocalCenter;
                var distanceToFloatingPoint = localCapsuleCentre.y * _transform.localScale.y - hit.distance;

                if (distanceToFloatingPoint == 0f)
                    return;
                
                
                var amountToLift = distanceToFloatingPoint * _resizableCapsuleCollider.SlopeData.StepReachForce - GetVerticalVelocity();

                var liftForce = new Vector3(0f, amountToLift, 0f);
                
                _component.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }
    }
}
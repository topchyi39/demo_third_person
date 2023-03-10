using UnityEngine;

namespace Player.Components.MovementComponents.States.GroundStates
{
    public class GroundState : BaseMoveState
    {
        public GroundState(MovementComponent component) : base(component)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            
            _component.Animator.SetFloat(_animationData.VerticalKey, _reusableData.MoveAxis.y);
            _component.Animator.SetFloat(_animationData.HorizontalKey, _reusableData.MoveAxis.x);
        }

        public override void Update()
        {
            base.Update();
            
            _component.Animator.SetFloatSmooth(_animationData.VerticalKey, _reusableData.MoveAxis.y);
            _component.Animator.SetFloatSmooth(_animationData.HorizontalKey, _reusableData.MoveAxis.x);
            
            if(_currentMovementStateData != null)
            {
                var direction = Mathf.Clamp(_reusableData.LookDelta.x, -1f, 1f);
                _component.Animator.SetFloatSmooth(Animator.StringToHash("Direction"), direction,
                    _currentMovementStateData.TimeToReachRotation.y);
            }
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
    }
}
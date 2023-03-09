using FiniteStateMachine;
using Player.Utility.Collider;
using UnityEngine;

namespace Player.Components.MovementComponents.States
{
    public class BaseMoveState : State
    {
        protected ResizableCapsuleCollider _resizableCapsuleCollider;
        protected MovementComponent _component;
        protected Transform _transform;

        public BaseMoveState(MovementComponent component)
        {
            _component = component;
            _resizableCapsuleCollider = _component.ResizableCapsuleCollider;
            _transform = _component.transform;
        }
        
        public override void Enter()
        {
            
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            Move();
        }

        public override void Exit()
        {
        }

        private void Move()
        {
            
        }
        
        protected float GetVerticalVelocity()
        {
            return _component.Rigidbody.velocity.y;
        }
    }
}
using FiniteStateMachine;
using Player.Utility.Collider;
using UnityEngine;

namespace Player.Components.MovementComponents
{
    [RequireComponent(typeof(ResizableCapsuleCollider))]
    public class MovementComponent : CharacterComponent
    {
        [SerializeField] private MovementLayerData layerData;

        private Rigidbody _rigidbody;
        private ResizableCapsuleCollider _resizableCapsuleCollider;
        private StateMachine _stateMachine;

        public ResizableCapsuleCollider ResizableCapsuleCollider => _resizableCapsuleCollider;
        public Rigidbody Rigidbody => _rigidbody;
        public MovementLayerData LayerData => layerData;

        public override void SetupAction()
        {
            _resizableCapsuleCollider = GetComponent<ResizableCapsuleCollider>();
            _rigidbody = GetComponent<Rigidbody>();
            
            _stateMachine = new MovementStateMachine(this);
        }

        public override void ExecuteUpdate()
        {
            _stateMachine.Update();
        }
        
        public override void ExecuteFixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }
    }
}
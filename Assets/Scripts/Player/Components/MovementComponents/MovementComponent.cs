using FiniteStateMachine;
using Player.Components.MovementComponents.Data;
using Player.Components.MovementComponents.States.Data;
using Player.Components.MovementComponents.Utility.Collider;
using UnityEngine;

namespace Player.Components.MovementComponents
{
    [RequireComponent(typeof(ResizableCapsuleCollider))]
    public class MovementComponent : CharacterComponent
    {
        [SerializeField] private MovementData movementData;
        [SerializeField] private MovementLayerData layerData;
        [SerializeField] private MovementAnimationData animationData;
        
        [Header("References")]
        [SerializeField] private Transform followTarget;
        [SerializeField] private Transform cameraTransform;
        
        private Rigidbody _rigidbody;
        private ResizableCapsuleCollider _resizableCollider;
        private MovementStateMachine _stateMachine;
        private ReusableData _reusableData;

        public MovementData MovementData => movementData;
        public MovementAnimationData AnimationData => animationData;
        public MovementLayerData LayerData => layerData;
        public Transform CameraTransform => cameraTransform;
        
        public Rigidbody Rigidbody => _rigidbody;
        public ResizableCapsuleCollider ResizableCollider => _resizableCollider;
        public MovementStateMachine StateMachine => _stateMachine;
        public ReusableData ReusableData => _reusableData;

        public override void SetupAction()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _resizableCollider = GetComponent<ResizableCapsuleCollider>();
            _reusableData = new ReusableData();
            _stateMachine = new MovementStateMachine(this);
        }
        
        

        public override void ExecuteUpdate()
        {
            _reusableData.MoveAxis = _input.MoveAxis.ReadValue<Vector2>();
            _reusableData.LookDelta = _input.Look.ReadValue<Vector2>();
            _stateMachine.Update();
        }
        
        public override void ExecuteFixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }
    }
}
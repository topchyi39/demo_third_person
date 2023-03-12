using FiniteStateMachine;
using Player.Components.MovementComponents.Data;
using Player.Components.MovementComponents.States.Data;
using Player.Components.MovementComponents.Utility.Collider;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private bool _useGamepad;

        private bool _lastShouldWalkKeyboard;

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

            _input.KeyboardActivated += KeyboardActivated;
            _input.GamepadActivated += GamepadActivated;
        }

        public override void ExecuteUpdate()
        {
            var moveValue = _input.MoveAxis.ReadValue<Vector2>();

            if (_useGamepad)
            {
                Debug.LogError(moveValue.magnitude < movementData.MaxMagnitudeForWalk);
                _reusableData.ShouldWalk = moveValue.magnitude < movementData.MaxMagnitudeForWalk;
                
                moveValue.Normalize();
            }
            
            _reusableData.MoveAxis = moveValue;
            _reusableData.LookDelta = _input.Look.ReadValue<Vector2>();
            
            _stateMachine.Update();
        }
        
        public override void ExecuteFixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }
        
        private void GamepadActivated()
        {
            _lastShouldWalkKeyboard = _reusableData.ShouldWalk;
            _useGamepad = true;
        }

        private void KeyboardActivated()
        {
            _useGamepad = false;
            _reusableData.ShouldWalk = _lastShouldWalkKeyboard;
        }
        
        public void OnAnimationEnterEvent()
        {
            _stateMachine.OnAnimationEnterEvent();
        }
        
        public void OnAnimationExitEvent()
        {
            _stateMachine.OnAnimationExitEvent();
        }
        
        public void OnAnimationTransitionEvent()
        {
            _stateMachine.OnAnimationTransitionEvent();
        }
    }
}
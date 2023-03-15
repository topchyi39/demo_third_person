using System;
using FiniteStateMachine;
using Player.Components.MovementComponents.Data;
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
        private CharacterResizableCapsuleCollider _resizableCollider;
        private MovementStateMachine _stateMachine;
        private ReusableData _reusableData;

        private bool _useGamepad;

        private bool _lastShouldWalkKeyboard;

        public MovementData MovementData => movementData;
        public MovementAnimationData AnimationData => animationData;
        public MovementLayerData LayerData => layerData;
        public Transform CameraTransform => cameraTransform;
        
        public Rigidbody Rigidbody => _rigidbody;
        public CharacterResizableCapsuleCollider ResizableCollider => _resizableCollider;
        public MovementStateMachine StateMachine => _stateMachine;
        public ReusableData ReusableData => _reusableData;

        public override void SetupAction()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _resizableCollider = GetComponent<CharacterResizableCapsuleCollider>();
            
            _reusableData = new ReusableData();
            _stateMachine = new MovementStateMachine(this);

            _input.KeyboardActivated += KeyboardActivated;
            _input.GamepadActivated += GamepadActivated;
            
            if(_input.IsGamepadActivated)
                GamepadActivated();
        }

        public override void ExecuteUpdate()
        {
            if (_input.MoveAxis.enabled) 
            {
                var moveValue = _input.MoveAxis.ReadValue<Vector2>();

                if (_useGamepad)
                {
                    _reusableData.ShouldWalk = moveValue.magnitude < movementData.MaxMagnitudeForWalk;

                    moveValue.Normalize();
                }

                _reusableData.MoveAxis = moveValue;

                if (moveValue.magnitude > 0.01f)
                    _reusableData.LastMoveAxis = moveValue;
            }
            
            _reusableData.LookDelta = _input.Look.ReadValue<Vector2>();
            
            _stateMachine.Update();
        }
        
        public override void ExecuteFixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        private void OnTriggerEnter(Collider other)
        {
            _stateMachine.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _stateMachine.OnTriggerExit(other);
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
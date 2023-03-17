using Player.Components.MovementComponents.States.GroundStates.MovingStates;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates.RollingStates
{
    public class RollingState : GroundState
    {
        private Vector3 _direction;
        private Vector2 _statAxis;

        public RollingState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _statAxis = _reusableData.MoveAxis;
            _reusableData.SpeedModifier = _groundedData.RollData.SpeedModifier;
            _reusableData.MovementDecelerationForce = _groundedData.RollData.DecelerationForce;
            _component.ResizableCollider.UpdateSetting(_groundedData.CrouchCapsuleSettings);
            if (_reusableData.MoveAxis.y != 0)
            {
                
                if(_reusableData.MoveAxis.y < 0)
                    _direction = CalculateDirection(true, true);
                else
                    _direction = CalculateDirection(true);


                RotateToDirection(_direction);
               
            }
        }

        public override void Exit()
        {
            base.Exit();

            _reusableData.MoveAxis = _component.CharacterInput.MoveAxis.ReadValue<Vector2>();
            _component.ResizableCollider.UpdateSetting(_groundedData.DefaultCapsuleSettings);
        }

        public override void Update()
        {
            base.Update();
            
            _reusableData.MoveAxis = _statAxis;
        }

        protected override void ChangeToMovingState()
        {
            BaseMoveState state;
            
            if(_reusableData.ShouldWalk)
                state = _component.StateMachine.WalkState;
            else
            {
                if (_reusableData.ShouldDash)
                    state = _component.StateMachine.DashState;
                else
                    state = _component.StateMachine.JogState;
            }

            if (_reusableData.ShouldCrouch)
                state = _component.StateMachine.CrouchState;
            
            _component.StateMachine.ChangeState(state);
        }

        //protected override void Rotate() { }

        #region Input Callbacks

        protected override void JumpPerformed(InputAction.CallbackContext obj)
        {
        }
        
        #endregion

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.RollKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.RollKey, false);
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();
            
            Debug.LogError("OnAnimationTransitionEvent");
            
            var axis = _component.CharacterInput.MoveAxis.ReadValue<Vector2>();
            
            if(axis != Vector2.zero)
                ChangeToMovingState();
            else
                _component.StateMachine.ChangeState(_component.StateMachine.JogStoppingState);
        }

        #endregion
        
    }
}
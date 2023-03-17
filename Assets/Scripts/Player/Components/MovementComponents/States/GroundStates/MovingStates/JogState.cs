using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates.MovingStates
{
    public class JogState : MovingState
    {
        public JogState(MovementComponent component) : base(component)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            _reusableData.SpeedModifier = _groundedData.JogData.SpeedModifier;
            _reusableData.JumpForce = _airborneData.JumpData.MediumForce;
            _reusableData.TimeToReachRotation = _groundedData.JogData.TimeToReachRotation;
        }

        public override void Update()
        {
            base.Update();
            
            if (_reusableData.ShouldWalk)
                _component.StateMachine.ChangeState(_component.StateMachine.WalkState);
        }

        #region Input Callbacks
        
        protected override async void MoveCanceled(InputAction.CallbackContext context)
        {
            base.MoveCanceled(context);
            
            await Task.Delay(250);
            
            if(_reusableData.MoveAxis == Vector2.zero)
                _component.StateMachine.ChangeState(_component.StateMachine.JogStoppingState);
        }

        protected override void WalkToggled(InputAction.CallbackContext context)
        {
            base.WalkToggled(context);
            
            if(_reusableData.ShouldWalk)
                _component.StateMachine.ChangeState(_component.StateMachine.WalkState);
        }

        #endregion
        
        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.JogKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.JogKey, false);
        }

        #endregion
    }
}
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates.MovingStates
{
    public class WalkState : MovingState
    {
        public WalkState(MovementComponent component) : base(component) { }
        
        public override void Enter()
        {
            base.Enter();

            _reusableData.SpeedModifier = _groundedData.WalkData.SpeedModifier;
            _reusableData.JumpForce = _airborneData.JumpData.WeakForce;
            _reusableData.TimeToReachRotation = _groundedData.WalkData.TimeToReachRotation;
        }

        public override void Update()
        {
            base.Update();
            
            if (!_reusableData.ShouldWalk)
                _component.StateMachine.ChangeState(_component.StateMachine.JogState);

        }

        #region Input Callbacks
        
        protected override void MoveCanceled(InputAction.CallbackContext context)
        {
            base.MoveCanceled(context);

            ToStopping();
        }
        
        protected override void WalkToggled(InputAction.CallbackContext context)
        {
            base.WalkToggled(context);
            
            if(!_reusableData.ShouldWalk)
                _component.StateMachine.ChangeState(_component.StateMachine.JogState);
        }

        protected virtual async void ToStopping()
        {
            await Task.Delay(250);
            
            if(_reusableData.MoveAxis == Vector2.zero)
                _component.StateMachine.ChangeState(_component.StateMachine.WalkStoppingState);
        }

        #endregion
        
        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.WalkKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.WalkKey, false);
        }

        #endregion
    }
}
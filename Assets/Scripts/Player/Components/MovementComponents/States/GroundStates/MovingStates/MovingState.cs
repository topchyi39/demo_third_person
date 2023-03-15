using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates.MovingStates
{
    public class MovingState : GroundState
    {
        private bool _canUpdateAnimationValues;
        
        public MovingState(MovementComponent component) : base(component)
        {
        }

        public override void Update()
        {
            base.Update();
            
            if(_reusableData.CanUpdateMoveAnimation)
            {
                _component.Animator.SetFloatSmooth(_animationData.VerticalKey, _reusableData.MoveAxis.y);
                _component.Animator.SetFloatSmooth(_animationData.HorizontalKey, _reusableData.MoveAxis.x);
            }
            
            var turn = Mathf.Clamp(_reusableData.LookDelta.x, -1f, 1f);
            _component.Animator.SetFloatSmooth(
                _animationData.TurnKey, 
                turn,
                _reusableData.TimeToReachRotation.y);
        }

        #region Input Callbacks

        protected override void CrouchPerformed(InputAction.CallbackContext context)
        {
            base.CrouchPerformed(context);
            
            if (_reusableData.ShouldCrouch)
                _component.StateMachine.ChangeState(_component.StateMachine.CrouchState);
        }

        protected override void MoveCanceled(InputAction.CallbackContext context)
        {
            base.MoveCanceled(context);
            
            _component.Animator.SetFloat(_animationData.VerticalKey, _reusableData.LastMoveAxis.y);
            _component.Animator.SetFloat(_animationData.HorizontalKey, _reusableData.LastMoveAxis.x);
        }

        protected override void DashPerformed(InputAction.CallbackContext context)
        {
            _reusableData.ShouldDash = !_reusableData.ShouldDash;
            
            if(_reusableData.ShouldDash)
                _component.StateMachine.ChangeState(_component.StateMachine.DashState);
        }

        #endregion

        #region Animation Methods
        
        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.MovingKey, true);

        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.MovingKey, false);
        }
        
        #endregion
    }
}
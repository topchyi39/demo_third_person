using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates
{
    public class IdleState : GroundState
    {
        public IdleState(MovementComponent component) : base(component) { }
        
        public override void Enter()
        {
            base.Enter();

            _reusableData.SpeedModifier = 0f;
        }
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            // if (!IsMovingHorizontally())
            // {
            //     return;
            // }

            ResetHorizontalVelocity();
        }

        #region Input Callbacks

        protected override void MovePerformed(InputAction.CallbackContext context)
        {
            var axis = _reusableData.MoveAxis;
            
            _component.Animator.SetFloat(_animationData.VerticalKey, axis.y);
            _component.Animator.SetFloat(_animationData.HorizontalKey, axis.x);
            
            ChangeToMovingState();
            base.MovePerformed(context);
        }

        #endregion

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.IdleKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.IdleKey, false);
        }

        #endregion

        
        
    }
}
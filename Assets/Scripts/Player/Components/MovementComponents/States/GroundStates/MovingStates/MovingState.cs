using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Components.MovementComponents.States.GroundStates.MovingStates
{
    public class MovingState : GroundState
    {
        public MovingState(MovementComponent component) : base(component)
        {
        }

        public override void Update()
        {
            base.Update();
            
            _component.Animator.SetFloatSmooth(_animationData.VerticalKey, _reusableData.MoveAxis.y);
            _component.Animator.SetFloatSmooth(_animationData.HorizontalKey, _reusableData.MoveAxis.x);
            
            var turn = Mathf.Clamp(_reusableData.LookDelta.x, -1f, 1f);
            _component.Animator.SetFloatSmooth(
                _animationData.TurnKey, 
                turn,
                _reusableData.TimeToReachRotation.y);
        }

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
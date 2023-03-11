using UnityEngine;

namespace Player.Components.MovementComponents.States.GroundStates.StoppingStates
{
    public class StoppingState : GroundState
    {
        public StoppingState(MovementComponent component) : base(component)
        {
        }

        public override void Update()
        {
            base.Update();

            if (_reusableData.MoveAxis != Vector2.zero)
            {
                _component.StateMachine.ChangeState(_component.StateMachine.WalkState);
                // add other states
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if (!IsMovingHorizontally())
            {
                return;
            }

            DecelerateHorizontally();
        }

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(_animationData.StoppingKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(_animationData.StoppingKey, false);
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();
            
            _component.StateMachine.ChangeState(_component.StateMachine.IdleState);
        }

        #endregion
    }
}
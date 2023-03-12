using Player.Components.MovementComponents.States.GroundStates.MovingStates;
using UnityEngine;

namespace Player.Components.MovementComponents.States.GroundStates.StartingStates
{
    public class StartingState : GroundState
    {
        protected virtual AnimationCurve modifierCurve { get; }
        protected virtual float speedModifier { get; }

        protected virtual int moveKey { get; }
        protected virtual MovingState nextState { get; }
        
        private float _time;
        
        public StartingState(MovementComponent component) : base(component)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _reusableData.SpeedModifier = modifierCurve.Evaluate(0) * speedModifier;
            _time = 0f;
        }

        public override void Update()
        {
            _reusableData.SpeedModifier = modifierCurve.Evaluate(_time) * speedModifier;
            _time += Time.deltaTime;
            
            base.Update();
        }

        #region Animation Methods

        protected override void StartStateAnimation()
        {
            base.StartStateAnimation();
            
            _component.Animator.SetBool(moveKey, true);
            _component.Animator.SetBool(_animationData.StartingKey, true);
        }

        protected override void EndStateAnimation()
        {
            base.EndStateAnimation();
            
            _component.Animator.SetBool(moveKey, false);
            _component.Animator.SetBool(_animationData.StartingKey, false);

        }
        
        public override void OnAnimationTransitionEvent()
        {
            if(_reusableData.MoveAxis != Vector2.zero)
                _component.StateMachine.ChangeState(nextState);
            else
            {
                _component.StateMachine.ChangeState(_component.StateMachine.IdleState);
            }
        }

        #endregion
    }
}
using FiniteStateMachine;
using Player.Components.MovementComponents.States;
using Player.Components.MovementComponents.States.GroundStates;
using Player.Components.MovementComponents.States.GroundStates.MovingStates;
using Player.Components.MovementComponents.States.GroundStates.StartingStates;
using Player.Components.MovementComponents.States.GroundStates.StoppingStates;

namespace Player.Components.MovementComponents
{
    public class MovementStateMachine : StateMachine
    {
        private MovementComponent _component;

        private BaseMoveState _defaultState;
        
        public IdleState IdleState { get; private set; }
        
        //Walk States
        public WalkStartingState WalkStartingState { get; private set; }
        public WalkState WalkState { get; private set; }
        public WalkStoppingState WalkStoppingState { get; private set; }
        
        //Jog States
        public JogStartingState JogStartingState { get; private set; }
        public JogState JogState { get; private set; }
        public JogStoppingState JogStoppingState { get; private set; }
        
        public MovementStateMachine(MovementComponent component)
        {
            _component = component;
            InitializeStates();
            ChangeState(IdleState);
        }

        private void InitializeStates()
        {
            _defaultState = new GroundState(_component);
            IdleState = new IdleState(_component);
            
            WalkStartingState = new WalkStartingState(_component);
            WalkState = new WalkState(_component);
            WalkStoppingState = new WalkStoppingState(_component);

            JogStartingState = new JogStartingState(_component);
            JogState = new JogState(_component);
            JogStoppingState = new JogStoppingState(_component);
        }

        public void OnAnimationEnterEvent()
        {
            ((BaseMoveState)_currentState).OnAnimationEnterEvent();
        }

        public void OnAnimationExitEvent()
        {
            ((BaseMoveState)_currentState).OnAnimationExitEvent();
        }

        public void OnAnimationTransitionEvent()
        {
            ((BaseMoveState)_currentState).OnAnimationTransitionEvent();
        }
    }
}
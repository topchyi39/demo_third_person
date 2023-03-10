using FiniteStateMachine;
using Player.Components.MovementComponents.States;
using Player.Components.MovementComponents.States.GroundStates;

namespace Player.Components.MovementComponents
{
    public class MovementStateMachine : StateMachine
    {
        private MovementComponent _component;

        private BaseMoveState _defaultState;
        
        public IdleState IdleState { get; private set; }
        public WalkState WalkState { get; private set; }
        
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
            WalkState = new WalkState(_component);
        }
    }
}
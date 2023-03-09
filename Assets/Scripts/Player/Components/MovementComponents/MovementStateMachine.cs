using FiniteStateMachine;
using Player.Components.MovementComponents.States;
using Player.Components.MovementComponents.States.GroundStates;

namespace Player.Components.MovementComponents
{
    public class MovementStateMachine : StateMachine
    {
        private MovementComponent _component;

        private BaseMoveState _defaultState;
        
        public MovementStateMachine(MovementComponent component)
        {
            _component = component;
            _defaultState = new GroundState(_component);
            
            ChangeState(_defaultState);
        }
    }
}
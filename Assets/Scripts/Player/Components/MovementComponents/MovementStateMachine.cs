using FiniteStateMachine;
using Player.Components.MovementComponents.States;
using Player.Components.MovementComponents.States.AirborneStates;
using Player.Components.MovementComponents.States.GroundStates;
using Player.Components.MovementComponents.States.GroundStates.CrouchingStates;
using Player.Components.MovementComponents.States.GroundStates.LandingStates;
using Player.Components.MovementComponents.States.GroundStates.MovingStates;
using Player.Components.MovementComponents.States.GroundStates.RollingStates;
using Player.Components.MovementComponents.States.GroundStates.StartingStates;
using Player.Components.MovementComponents.States.GroundStates.StoppingStates;
using UnityEngine;

namespace Player.Components.MovementComponents
{
    public class MovementStateMachine : StateMachine
    {
        private MovementComponent _component;

        private BaseMoveState _defaultState;
        
        public IdleState IdleState { get; private set; }

        #region Garounded States
        
        //Walk States
        public WalkStartingState WalkStartingState { get; private set; }
        public WalkState WalkState { get; private set; }
        public WalkStoppingState WalkStoppingState { get; private set; }
        
        //Jog States
        public JogStartingState JogStartingState { get; private set; }
        public JogState JogState { get; private set; }
        public JogStoppingState JogStoppingState { get; private set; }
        
        //Dash States
        public DashStartingState DashStartingState { get; private set; }
        public DashState DashState { get; private set; }
        public DashStoppingState DashStoppingState { get; private set; }
        
        public RollingState RollingState { get; private set; }
        
        public CrouchIdleState CrouchIdleState { get; private set; }
        public CrouchState CrouchState { get; private set; }
        public CrouchStoppingState CrouchStoppingState { get; private set; }
        
        //Landing States
        public CrouchLandState CrouchLandState { get; private set; }
        public LightLandState LightLandState { get; private set; }
        public HardLandState HardLandState { get; private set; }
        
        #endregion

        #region Airborne States

        public FallState FallState { get; private set; }
        public JumpState JumpState { get; private set; }
        
        #endregion
        
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

            DashStartingState = new DashStartingState(_component);
            DashState = new DashState(_component);
            DashStoppingState = new DashStoppingState(_component);

            RollingState = new RollingState(_component);

            CrouchIdleState = new CrouchIdleState(_component);
            CrouchState = new CrouchState(_component);
            CrouchStoppingState = new CrouchStoppingState(_component);

            CrouchLandState = new CrouchLandState(_component);
            LightLandState = new LightLandState(_component);
            HardLandState = new HardLandState(_component);

            FallState = new FallState(_component);
            JumpState = new JumpState(_component);
        }
        
        public void OnTriggerEnter(Collider other)
        {
            _currentState.OnTriggerEnter(other);
        }
        
        public void OnTriggerExit(Collider other)
        {
            _currentState.OnTriggerExit(other);
        }
        
        public void OnAnimationEnterEvent()
        {
            _currentState.OnAnimationEnterEvent();
        }

        public void OnAnimationExitEvent()
        {
            _currentState.OnAnimationExitEvent();
        }

        public void OnAnimationTransitionEvent()
        {
            _currentState.OnAnimationTransitionEvent();
        }
    }
}
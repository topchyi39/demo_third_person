using System;
using UnityEngine;

namespace FiniteStateMachine
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void Exit();
    }
    
    public class StateMachine
    {
        private State _currentState;
        
        public void ChangeState(State state)
        {
            if (state == null) return;
            
            _currentState?.Exit();

            _currentState = state;
            _currentState.Enter();
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }
    }
}
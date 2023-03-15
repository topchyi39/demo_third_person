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

        public abstract void OnTriggerEnter(Collider other);
        public abstract void OnTriggerExit(Collider other);
        
        public abstract void OnAnimationEnterEvent();
        public abstract void OnAnimationExitEvent();
        public abstract void OnAnimationTransitionEvent();
    }
    
    public class StateMachine
    {
        protected State _currentState;
        
        public void ChangeState(State state)
        {
            if (state == null) return;
            
            _currentState?.Exit();
            Debug.Log($"Change to {state.GetType().Name}");
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
using System;
using System.Collections.Generic;

namespace BoardsRegulation
{
    public class StateMachine
    {
        private State _currentState;

        private Dictionary<Type, State> _states = new Dictionary<Type, State>();

        public void AddState(State state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : State
        {
            var type = typeof(T);

            if (_currentState != null && _currentState.GetType() == type)
                return;

            if (_states.TryGetValue(type, out var state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();

            }
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}

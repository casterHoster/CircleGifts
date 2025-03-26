using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private BoardState _currentState;

    private Dictionary<Type, BoardState> _states = new Dictionary<Type, BoardState>();

    public void AddState(BoardState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : BoardState
    {
        var type = typeof(T);

        if (_currentState.GetType() == type)
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

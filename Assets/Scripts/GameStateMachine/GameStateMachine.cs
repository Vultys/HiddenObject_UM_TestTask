using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStateMachine
{
    private readonly Dictionary<GameStateType, IGameState> _states;
    private IGameState _currentState;

    public GameStateMachine(IEnumerable<IGameState> states)
    {
        _states = states.ToDictionary(state => state.Type);
    }

    /// <summary>
    /// Changes the current state to the given state
    /// </summary>
    /// <param name="newState"> The new state to change to </param>
    public void ChangeState(GameStateType newState)
    {
        if(_currentState?.Type == newState) return;

        _currentState?.Exit();
        if(_states.TryGetValue(newState, out var nextState))
        {
            _currentState = nextState;
            _currentState.Enter(this);
        }
        else
        {
            Debug.LogWarning($"Could not find state {newState}");
        }
    }
}

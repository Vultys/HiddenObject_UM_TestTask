public interface IGameState
{
    public GameStateType Type { get; }

    /// <summary>
    /// Called when the state is entered
    /// </summary>
    /// <param name="gameStateMachine"> The game state machine </param>
    public void Enter(GameStateMachine gameStateMachine);

    /// <summary>
    /// Called when the state is exited
    /// </summary>
    public void Exit();
}
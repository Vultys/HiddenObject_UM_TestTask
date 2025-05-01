using UniRx;
using Zenject;

/// <summary>
/// The playing state
/// </summary>
public class PlayingState : IGameState
{
    [Inject] private readonly GameView _gameView;
    [Inject] private readonly GameViewModel _gameViewModel;
    private readonly CompositeDisposable _disposables = new();

    public GameStateType Type => GameStateType.Playing;

    public void Enter(GameStateMachine gameStateMachine)
    {
        _gameView.Show();
        _gameViewModel.StartGame();
        
        _gameView.OnUseHint
            .AsObservable()
            .Subscribe(_ => UseHint())
            .AddTo(_disposables);
    }

    public void Exit()
    {
        _disposables.Clear();
        _gameView.Hide();
    }

    /// <summary>
    /// Uses the hint
    /// </summary>
    public void UseHint()
    {
        _gameViewModel.UseHintCommand?.Execute();
    }
}

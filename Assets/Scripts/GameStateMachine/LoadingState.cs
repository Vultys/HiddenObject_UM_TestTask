using System;
using System.Diagnostics;
using UniRx;
using Zenject;

/// <summary>
/// The loading state
/// </summary>
public class LoadingState : IGameState
{
    private readonly LoadingView _loadingView;

    private readonly TimeSpan _loadingDuration = TimeSpan.FromSeconds(2);

    public GameStateType Type => GameStateType.Loading;

    [Inject]
    public LoadingState(LoadingView loadingView)
    {
        _loadingView = loadingView;
    }

    public void Enter(GameStateMachine gameStateMachine)
    {
        _loadingView.Show();

        Observable.Timer(_loadingDuration)
            .Subscribe(_ => gameStateMachine.ChangeState(GameStateType.Menu))
            .AddTo(_loadingView);
    }

    public void Exit()
    {
        _loadingView.Hide();
    }
}
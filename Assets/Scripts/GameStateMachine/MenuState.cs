using UniRx;
using Zenject;

/// <summary>
/// The menu state
/// </summary>
public class MenuState : IGameState
{
    private readonly CompositeDisposable _disposables = new();
    private readonly MenuView _menuView;

    private GameStateMachine _gameStateMachine;
    
    public GameStateType Type => GameStateType.Menu;

    [Inject]
    public MenuState(MenuView menuView)
    {
        _menuView = menuView;
    }

    public void Enter(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
        _menuView.Show();

        _menuView.OnStartGame
            .AsObservable()
            .Subscribe(_ => _gameStateMachine.ChangeState(GameStateType.Playing))
            .AddTo(_disposables);
    }

    public void Exit()
    {
        _menuView.Hide();
        _disposables.Clear();
    }
}

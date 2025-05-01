using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;

public class GameViewModel : IReactiveViewModel<GameModel>, IDisposable
{
    public GameModel Model { get; private set; }
    public IReadOnlyList<HiddenObjectViewModel> HiddenObjects => _viewModels;
    public IReadOnlyReactiveProperty<int> FoundCount => _foundCount;


    public ReactiveCommand UseHintCommand = new();
    private readonly ReactiveProperty<int> _foundCount = new(0);

    private List<HiddenObjectViewModel> _viewModels = new();
    private readonly CompositeDisposable _disposables = new();
    private HiddenObjectSpawner _hiddenObjectSpawner;
    private GameStateMachine _gameStateMachine;

    [Inject]
    public void Construct(
        GameModel model, 
        HiddenObjectSpawner hiddenObjectSpawner, 
        GameStateMachine gameStateMachine, 
        HintService hintService)
    {
        Model = model;        
        _hiddenObjectSpawner = hiddenObjectSpawner;
        _gameStateMachine = gameStateMachine;

        InitializeHiddenObjects(model);
        BindCommands(hintService);

        _gameStateMachine.ChangeState(GameStateType.Loading);
    }

    /// <summary>
    /// Initializes the hidden objects
    /// </summary>
    /// <param name="model"> The game model </param>
    private void InitializeHiddenObjects(GameModel model)
    {
        _viewModels = model.HiddenObjects
            .Select(hiddenObject => 
            {
                var viewModel = new HiddenObjectViewModel(hiddenObject);
                viewModel.IsFound
                    .Where(found => found)
                    .Subscribe(OnFound)
                    .AddTo(_disposables);
                return viewModel;
            })
            .ToList();
    }

    /// <summary>
    /// Binds the commands
    /// </summary>
    /// <param name="hintService"> The hint service </param>
    private void BindCommands(HintService hintService)
    {
        UseHintCommand.Subscribe(_ => hintService.UseHint())
            .AddTo(_disposables);
    }

    /// <summary>
    /// Starts the game
    /// </summary>
    public void StartGame()
    {
        _hiddenObjectSpawner.SpawnAll();
    }

    /// <summary>
    /// Handles the found event
    /// </summary>
    /// <param name="isFound"> True if the hidden object was found </param>
    private void OnFound(bool isFound)
    {
        _foundCount.Value++;
        if(_foundCount.Value == HiddenObjects.Count)
        {
            CompleteGame();
        }
    }
    
    /// <summary>
    /// Completes the game
    /// </summary>
    private void CompleteGame()
    {
        _hiddenObjectSpawner.DespawnAll();
        _gameStateMachine.ChangeState(GameStateType.Menu);
        _foundCount.Value = 0;
    }
    
    public void Dispose()
    {
        _disposables?.Dispose();
    }
}

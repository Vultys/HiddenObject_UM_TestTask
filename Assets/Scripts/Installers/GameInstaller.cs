using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Hidden Objects")]
    [SerializeField] private List<HiddenObjectScriptable> _hiddenObjectScriptables;
    [SerializeField] private HiddenObjectSpawner _hiddenObjectSpawner;
    [SerializeField] private HiddenObjectView _hiddenObjectPrefab;
    [SerializeField] private Transform _hiddenObjectsParent;

    [Header("Views")]
    [SerializeField] private LoadingView _loadingView;
    [SerializeField] private MenuView _menuView;
    [SerializeField] private GameView _gameView;
    
    public override void InstallBindings()
    {
        BindHiddenObjects();
        BindGameModels();
        BindViews();
        BindHintSystem();
        BindGameStates();
        BindGameStateMachine();
    }

    /// <summary>
    /// Binds the hidden objects to the game model and the object pool
    /// </summary>
    private void BindHiddenObjects()
    {
        var hiddenModels = _hiddenObjectScriptables.Select(s =>
            new HiddenObjectModel(s.name, s.DisplayName, s.AssetAdress)
        ).ToList();

        Container.Bind<GameModel>()
            .FromInstance(new GameModel(hiddenModels))
            .AsSingle();

        Container.BindInstance(_hiddenObjectSpawner);

        Container.BindMemoryPool<HiddenObjectView, HiddenObjectViewPool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefab(_hiddenObjectPrefab)
            .UnderTransform(_hiddenObjectsParent);
    }

    /// <summary>
    /// Binds the game models to the game view model
    /// </summary>
    private void BindGameModels()
    {
        Container.Bind<GameViewModel>()
            .AsSingle()
            .NonLazy();
    }

    /// <summary>
    /// Binds the views to the game view model
    /// </summary>
    private void BindViews()
    {
        Container.BindInstance(_loadingView).AsSingle();
        Container.BindInstance(_menuView).AsSingle();
        Container.BindInstance(_gameView).AsSingle();
    }

    /// <summary>
    /// Binds the hint system to the game view model
    /// </summary>
    private void BindHintSystem()
    {
        Container.Bind<IHintStrategy>().To<ShowHintStrategy>().AsSingle();
        Container.Bind<HintService>().AsSingle();
    }

    /// <summary>
    /// Binds the game states to the game state machine
    /// </summary>
    private void BindGameStates()
    {
        Container.Bind<LoadingState>().AsSingle();
        Container.Bind<MenuState>().AsSingle();
        Container.Bind<PlayingState>().AsSingle();

        Container.Bind<IGameState>().To<LoadingState>().FromResolve();
        Container.Bind<IGameState>().To<MenuState>().FromResolve();
        Container.Bind<IGameState>().To<PlayingState>().FromResolve();
    }

    /// <summary>
    /// Binds the game state machine to the game states
    /// </summary>
    private void BindGameStateMachine()
    {
        Container.Bind<GameStateMachine>()
            .AsSingle()
            .WithArguments(Container.ResolveAll<IGameState>());
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;

public class GameViewModel : IReactiveViewModel<GameModel>, IDisposable
{
    public GameModel Model { get; private set; }

    public ReactiveProperty<GameState> State { get; } = new(GameState.Initializing);
    public ReactiveProperty<HintMode> CurrentHintMode { get; } = new(HintMode.None);

    private List<HiddenObjectViewModel> _viewModels = new();
    public IReadOnlyList<HiddenObjectViewModel> HiddenObjects => _viewModels;

    private readonly ReactiveProperty<int> _foundCount = new(0);
    public IReadOnlyReactiveProperty<int> FoundCount => _foundCount;

    private HiddenObjectSpawner _hiddenObjectSpawner;

    [Inject]
    public void Construct(GameModel model, HiddenObjectSpawner hiddenObjectSpawner)
    {
        _viewModels = model.HiddenObjects.Select(model =>
        {
            var vm = new HiddenObjectViewModel(model);
            vm.IsFound
                .Where(found => found)
                .Subscribe(OnFound)
                .AddTo(_disposables);
            return vm;
        }).ToList();

        _hiddenObjectSpawner = hiddenObjectSpawner;
        _hiddenObjectSpawner.SpawnAll();
        State.Value = GameState.Playing;
    }

    public void UseHint()
    {

    }

    public void CompleteGame()
    {
        State.Value = GameState.Completed;
    }

    private void OnFound(bool isFound)
    {
        _foundCount.Value++;
    }

    private CompositeDisposable _disposables = new();

    public void Dispose()
    {
        _disposables?.Dispose();
    }
}

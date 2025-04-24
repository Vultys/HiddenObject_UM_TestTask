using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

public class GameViewModel : IReactiveViewModel<GameModel>, IDisposable
{
    public GameModel Model { get; private set; }

    public ReactiveProperty<GameState> State { get; } = new(GameState.Initializing);
    public ReactiveProperty<HintMode> CurrentHintMode { get; } = new(HintMode.None);

    private List<HiddenObjectViewModel> _viewModels = new();
    public IReadOnlyList<HiddenObjectViewModel> HiddenObjects => _viewModels;

    public IReadOnlyReactiveProperty<int> FoundCount => _foundCount;

    private readonly ReactiveProperty<int> _foundCount = new(0);

    public void Init(GameModel model)
    {
        _viewModels = Model.HiddenObjects.Select(model =>
        {
            var vm = new HiddenObjectViewModel(model);
            vm.IsFound
                .Where(found => found)
                .Subscribe(_ => _foundCount.Value++)
                .AddTo(_disposables);
            return vm;
        }).ToList();

        State.Value = GameState.Playing;
    }

    public void UseHint()
    {

    }

    public void CompleteGame()
    {
        State.Value = GameState.Completed;
    }

    private CompositeDisposable _disposables = new();

    public void Dispose()
    {
        _disposables?.Dispose();
    }
}

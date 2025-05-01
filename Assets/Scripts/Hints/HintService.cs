public class HintService
{
    private readonly IHintStrategy _strategy;
    private readonly GameViewModel _gameViewModel;

    public HintService(IHintStrategy strategy, GameViewModel gameViewModel)
    {
        _strategy = strategy;
        _gameViewModel = gameViewModel;
    }
    
    /// <summary>
    /// Uses the hint
    /// </summary>
    public void UseHint()
    {
        _strategy.ExecuteHint(_gameViewModel.HiddenObjects);
    }
}

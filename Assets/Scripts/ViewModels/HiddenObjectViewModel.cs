using UniRx;

public class HiddenObjectViewModel : IReactiveViewModel<HiddenObjectModel>
{
    public HiddenObjectModel Model { get; }
    public IReadOnlyReactiveProperty<bool> IsFound => Model.IsFound;

    public ReactiveCommand HighlightCommand { get; } = new();
    public ReactiveCommand RevealCommand { get; } = new();

    public HiddenObjectViewModel(HiddenObjectModel model)
    {
        Model = model;
    }

    /// <summary>
    /// Marks the object as found
    /// </summary>
    public void MarkAsFound()
    {
        if(!Model.IsFound.Value)
        {
            Model.MarkAsFound();
        }
    }

    /// <summary>
    /// Resets the object to not found
    /// </summary>
    public void Reset()
    {
        Model.Reset();
    }
}

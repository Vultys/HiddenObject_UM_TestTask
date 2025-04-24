using UniRx;

public class HiddenObjectViewModel : IReactiveViewModel<HiddenObjectModel>
{
    public HiddenObjectModel Model { get; }

    public IReadOnlyReactiveProperty<bool> IsFound => Model.IsFound;

    public HiddenObjectViewModel(HiddenObjectModel model)
    {
        Model = model;
    }

    public void MarkAsFound()
    {
        if(!Model.IsFound.Value)
        {
            Model.MarkAsFound();
        }
    }
}

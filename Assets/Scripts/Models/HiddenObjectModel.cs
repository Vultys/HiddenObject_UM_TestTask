using UniRx;

public class HiddenObjectModel : IModel
{
    public string Id { get; private set; }
    public string DisplayName { get; private set; }
    public string AssetAdress { get; private set; }

    public ReactiveProperty<bool> IsFound { get; } = new(false);

    public HiddenObjectModel(string id, string displayName, string assetAdress)
    {
        Id = id;
        DisplayName = displayName;
        AssetAdress = assetAdress;
    }

    public void MarkAsFound()
    {
        IsFound.Value = true;
    }

    public void Reset()
    {
        IsFound.Value = false;
    }
}

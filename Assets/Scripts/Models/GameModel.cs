using System.Collections.Generic;

public class GameModel : IModel
{
    public string Id { get; }

    public List<HiddenObjectModel> HiddenObjects { get; }

    public GameModel(List<HiddenObjectModel> hiddenObjects)
    {
        HiddenObjects = hiddenObjects;
    }
}

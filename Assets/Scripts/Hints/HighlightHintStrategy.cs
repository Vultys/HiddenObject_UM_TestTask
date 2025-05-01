using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Highlights the next hidden object
/// </summary>
public class HighlightHintStrategy : IHintStrategy
{
    public void ExecuteHint(IEnumerable<HiddenObjectViewModel> hiddenObjects)
    {
        var nextHiddenObject = hiddenObjects.FirstOrDefault(obj => obj?.IsFound?.Value == false);

        nextHiddenObject?.HighlightCommand?.Execute();
    }
}
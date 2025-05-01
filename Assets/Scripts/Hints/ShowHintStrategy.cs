using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Shows the next hidden object
/// </summary>
public class ShowHintStrategy : IHintStrategy
{
    public void ExecuteHint(IEnumerable<HiddenObjectViewModel> hiddenObjects)
    {
        var nextHiddenObject = hiddenObjects.FirstOrDefault(obj => obj?.IsFound?.Value == false);

        nextHiddenObject?.RevealCommand?.Execute();
    }
}

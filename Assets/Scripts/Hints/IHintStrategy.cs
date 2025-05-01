using System.Collections.Generic;

public interface IHintStrategy
{
    /// <summary>
    /// Executes the hint strategy
    /// </summary>
    /// <param name="hiddenObjects"> The hidden objects </param>
    public void ExecuteHint(IEnumerable<HiddenObjectViewModel> hiddenObjects);
}

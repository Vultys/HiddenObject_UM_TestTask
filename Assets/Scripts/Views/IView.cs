public interface IView<in TViewModel>
{
    /// <summary>
    /// Initializes the view with the given view model
    /// </summary>
    /// <param name="viewModel"> The view model </param>
    public void Init(TViewModel viewModel);
}

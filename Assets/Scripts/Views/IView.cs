public interface IView<in TViewModel>
{
    public void Init(TViewModel viewModel);
}

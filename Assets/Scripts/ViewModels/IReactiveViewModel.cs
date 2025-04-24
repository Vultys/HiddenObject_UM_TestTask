public interface IReactiveViewModel<out TModel> where TModel : IReactiveModel
{
    public TModel Model { get; }
}

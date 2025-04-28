public interface IReactiveViewModel<out TModel> where TModel : IModel
{
    public TModel Model { get; }
}
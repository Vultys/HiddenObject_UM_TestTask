using UniRx;

public interface IViewModel<out TModel> where TModel : IModel
{
    public TModel Model { get; }

    public ReactiveProperty<bool> IsFound { get; }
}

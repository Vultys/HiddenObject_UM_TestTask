using UniRx;

public interface IReactiveModel
{
    public string Id { get; }
    public ReactiveProperty<bool> IsFound { get; }
}

using UniRx;

public interface IModel
{
    public string Id { get; }
    public ReactiveProperty<bool> IsFound { get; }
}

using UniRx;
using UnityEngine;

public class GameView : MonoBehaviour, IUIView
{
    [SerializeField] private GameObject _gameScreen;

    public ReactiveCommand OnUseHint = new();

    public void Show()
    {
        _gameScreen.SetActive(true);
    }

    public void Hide()
    {
        _gameScreen.SetActive(false);
    }

    public void UseHint()
    {
        OnUseHint?.Execute();
    }
}

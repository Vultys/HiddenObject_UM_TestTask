using System;
using UniRx;
using UnityEngine;

public class MenuView : MonoBehaviour, IUIView
{
    [SerializeField] private GameObject _menuScreen;

    public ReactiveCommand OnStartGame = new();

    public void Show()
    {
        _menuScreen.SetActive(true);
    }

    public void Hide()
    {
        _menuScreen.SetActive(false);
    }

    public void StartGame()
    {
        OnStartGame.Execute();
    }

    public void RestartGame()
    {
        
    }
}

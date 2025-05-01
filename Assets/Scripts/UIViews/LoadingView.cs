using UnityEngine;

public class LoadingView : MonoBehaviour, IUIView
{
    [SerializeField] private GameObject _loadingScreen;

    public void Show()
    {
        _loadingScreen.SetActive(true);
    }

    public void Hide()
    {
        _loadingScreen.SetActive(false);
    }
}

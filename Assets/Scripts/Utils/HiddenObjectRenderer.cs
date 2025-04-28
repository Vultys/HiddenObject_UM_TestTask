using UnityEngine;
using Zenject;

public class HiddenObjectRenderer : MonoBehaviour
{
    [SerializeField] private Transform _parent;

    [SerializeField] private HiddenObjectView _hiddenObjectPrefab;

    private DiContainer _diContainer;
    private GameViewModel _gameViewModel;

    [Inject]
    public void Construct(DiContainer diContainer, GameViewModel gameViewModel)
    {
        _diContainer = diContainer;
        _gameViewModel = gameViewModel;
    }

    private void Start()
    {
        RenderHiddenObjects();   
    }

    private void RenderHiddenObjects()
    {
        foreach(var viewModel in _gameViewModel.HiddenObjects)
        {
            var view = _diContainer.InstantiatePrefabForComponent<HiddenObjectView>(_hiddenObjectPrefab, _parent);
            view.Init(viewModel);
        }
    }
}

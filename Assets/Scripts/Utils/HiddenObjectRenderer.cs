using TMPro;
using UnityEngine;
using Zenject;

public class HiddenObjectRenderer : MonoBehaviour
{
    [SerializeField] private Transform _parent;

    [SerializeField] private Transform _displayNamesParent;

    [SerializeField] private HiddenObjectView _hiddenObjectPrefab;

    [SerializeField] private TextMeshProUGUI _displayNamePrefab;

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
            var displayName = _diContainer.InstantiatePrefabForComponent<TextMeshProUGUI>(_displayNamePrefab, _displayNamesParent);
            view.Init(viewModel, displayName);
        }
    }
}

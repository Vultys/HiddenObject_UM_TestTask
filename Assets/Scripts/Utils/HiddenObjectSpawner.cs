using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class HiddenObjectSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform _spawnArea;
    [SerializeField] private float _minDistance = 50f;
    [SerializeField] private int _maxAttempts = 100;
    [SerializeField] private TextMeshProUGUI _hiddenObjectNamePrefab;
    [SerializeField] private Transform _hiddenObjectNameParent;

    private List<Vector2> _occupiedPositions = new();
    private DiContainer _diContainer;
    private GameViewModel _gameViewModel;
    private HiddenObjectViewPool _hiddenObjectViewPool;
    private List<HiddenObjectView> _spawnedViews = new();

    [Inject]
    public void Construct(DiContainer diContainer, GameViewModel gameViewModel, HiddenObjectViewPool hiddenObjectViewPool)
    {
        _diContainer = diContainer;
        _gameViewModel = gameViewModel;
        _hiddenObjectViewPool = hiddenObjectViewPool;
    }

    public void RespawnAll()
    {
        _occupiedPositions.Clear();
        foreach(var view in _spawnedViews)
        {
            _hiddenObjectViewPool.Despawn(view);
            view.DespawnText();
        }
        _spawnedViews.Clear();
        SpawnAll();
    }

    public void SpawnAll()
    {
        foreach(var hiddenObject in _gameViewModel.HiddenObjects)
        {
            Spawn(hiddenObject);
        }
    }

    public void Spawn(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Spawn(_gameViewModel.HiddenObjects[i]);
        }
    }

    private void Spawn(HiddenObjectViewModel model)
    {
        var position = FindFreePosition();
        if(position.HasValue)
        {
            var view = _hiddenObjectViewPool.Spawn(model);
            var name = _diContainer.InstantiatePrefabForComponent<TextMeshProUGUI>(_hiddenObjectNamePrefab, _hiddenObjectNameParent);
            view.transform.localPosition = position.Value;
            view.InitText(name);
            _spawnedViews.Add(view);
        }
        else
        {
            Debug.LogWarning("Could not find a free position for hidden object");
        }
    }

    private Vector2? FindFreePosition()
    {
        int attempt = 0;

        while(attempt < _maxAttempts)
        {
            attempt++;
            var randomPosition = new Vector2(
                Random.Range(_spawnArea.rect.xMin, _spawnArea.rect.xMax),
                Random.Range(_spawnArea.rect.yMin, _spawnArea.rect.yMax)
            );

            if(IsPositionFree(randomPosition))
            {
                return randomPosition;
            }
        }

        return null;
    }

    private bool IsPositionFree(Vector2 position)
    {
        foreach(var occupiedPosition in _occupiedPositions)
        {
            if(Vector2.Distance(occupiedPosition, position) < _minDistance)
            {
                return false;
            }
        }

        return true;
    }
}

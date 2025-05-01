using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class HiddenObjectSpawner : MonoBehaviour
{
    [Header("Spawn Area Settings")]
    [SerializeField] private RectTransform _spawnArea;
    [SerializeField] private float _minDistance = 50f;
    [SerializeField] private int _maxAttempts = 100;

    [Header("Hidden Object Name Settings")]
    [SerializeField] private TextMeshProUGUI _hiddenObjectNamePrefab;
    [SerializeField] private Transform _hiddenObjectNameParent;

    private readonly List<Vector2> _occupiedPositions = new();
    private readonly List<HiddenObjectView> _spawnedViews = new();

    private DiContainer _diContainer;
    private GameViewModel _gameViewModel;
    private HiddenObjectViewPool _hiddenObjectViewPool;

    [Inject]
    public void Construct(DiContainer diContainer, GameViewModel gameViewModel, HiddenObjectViewPool hiddenObjectViewPool)
    {
        _diContainer = diContainer;
        _gameViewModel = gameViewModel;
        _hiddenObjectViewPool = hiddenObjectViewPool;
    }

    /// <summary>
    /// Despawns all hidden objects
    /// </summary>
    public void DespawnAll()
    {
        _occupiedPositions.Clear();

        foreach(var view in _spawnedViews)
        {
            _hiddenObjectViewPool.Despawn(view);
            view.Reset();
            view.DespawnText();
        }
        
        _spawnedViews.Clear();
    }

    /// <summary>
    /// Spawns all hidden objects
    /// </summary>
    public void SpawnAll()
    {
        foreach(var hiddenObject in _gameViewModel.HiddenObjects)
        {
            Spawn(hiddenObject);
        }
    }

    /// <summary>
    /// Spawns a hidden object at a random position
    /// </summary>
    /// <param name="model"> The hidden object model </param>
    private void Spawn(HiddenObjectViewModel model)
    {
        var position = FindFreePosition();
        if(!position.HasValue)   
        {
            Debug.LogWarning("Could not find a free position for hidden object");
            return;
        }
        
        var view = _hiddenObjectViewPool.Spawn(model);
        var name = _diContainer.InstantiatePrefabForComponent<TextMeshProUGUI>(_hiddenObjectNamePrefab, _hiddenObjectNameParent);

        view.transform.localPosition = position.Value;
        view.InitText(name);

        _spawnedViews.Add(view);
        _occupiedPositions.Add(position.Value);
    }

    /// <summary>
    /// Finds a free position for the hidden object
    /// </summary>
    /// <returns> The free position </returns>
    private Vector2? FindFreePosition()
    {
        for(int attempt = 0; attempt < _maxAttempts; attempt++)
        {
            var randomPosition = GenerateRandomPosition();
            if(IsPositionFree(randomPosition))
            {
                return randomPosition;
            }
        }
        return null;
    }

    /// <summary>
    /// Generates a random position within the spawn area
    /// </summary>
    /// <returns> The random position </returns>
    private Vector2 GenerateRandomPosition()
    {
        return new Vector2(
            Random.Range(_spawnArea.rect.xMin + _minDistance, _spawnArea.rect.xMax - _minDistance),
            Random.Range(_spawnArea.rect.yMin + _minDistance, _spawnArea.rect.yMax - _minDistance)
        );
    }

    /// <summary>
    /// Checks if the position is free
    /// </summary>
    /// <param name="position"> The position to check </param>
    /// <returns> True if the position is free </returns>
    private bool IsPositionFree(Vector2 position)
    {
        return _occupiedPositions.TrueForAll(occupiedPosition => 
            Vector2.Distance(occupiedPosition, position) >= _minDistance);
    }
}

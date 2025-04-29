using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private List<HiddenObjectScriptable> _hiddenObjectScriptables;

    [SerializeField] private HiddenObjectSpawner _hiddenObjectSpawner;

    [SerializeField] private HiddenObjectView _hiddenObjectPrefab;

    [SerializeField] private Transform _hiddenObjectsParent;

    public override void InstallBindings()
    {
        var hiddenModels = _hiddenObjectScriptables.Select(s =>
            new HiddenObjectModel(s.name, s.DisplayName, s.AssetAdress)
        ).ToList();

        Container.Bind<GameModel>()
            .FromInstance(new GameModel(hiddenModels))
            .AsSingle();

        Container.Bind<GameViewModel>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInstance(_hiddenObjectSpawner);

        Container.BindMemoryPool<HiddenObjectView, HiddenObjectViewPool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefab(_hiddenObjectPrefab)
            .UnderTransform(_hiddenObjectsParent);
    }
}
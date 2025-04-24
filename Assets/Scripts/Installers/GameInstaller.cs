using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private List<HiddenObjectScriptable> hiddenObjectScriptables;

    public override void InstallBindings()
    {
        var hiddenModels = hiddenObjectScriptables.Select(s =>
            new HiddenObjectModel(s.name, s.DisplayName, s.AssetAdress)
        ).ToList();


        Container.Bind<GameModel>()
            .FromInstance(new GameModel(hiddenModels))
            .AsSingle();

        Container.Bind<GameViewModel>()
            .AsSingle()
            .NonLazy();
    }
}
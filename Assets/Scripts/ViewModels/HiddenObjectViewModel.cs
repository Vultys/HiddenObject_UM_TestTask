
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class HiddenObjectViewModel : IReactiveViewModel<HiddenObjectModel>
{
    public HiddenObjectModel Model { get; }

    public IReadOnlyReactiveProperty<bool> IsFound => Model.IsFound;

    private AsyncOperationHandle<Sprite> _spriteHandle;

    public HiddenObjectViewModel(HiddenObjectModel model)
    {
        Model = model;
    }

    public void MarkAsFound()
    {
        if(!Model.IsFound.Value)
        {
            Model.MarkAsFound();
        }
    }

    public IEnumerator InitView(Image image)
    {
        _spriteHandle = Addressables.LoadAssetAsync<Sprite>(Model.AssetAdress);
        yield return _spriteHandle;

        if(_spriteHandle.Status == AsyncOperationStatus.Succeeded)
        {
            image.sprite = _spriteHandle.Result;
        }
    }
}

using UnityEngine;
using Zenject;

public class HiddenObjectViewPool : MonoMemoryPool<HiddenObjectViewModel, HiddenObjectView>
{
    protected override void Reinitialize(HiddenObjectViewModel model, HiddenObjectView view)
    {
        view.Init(model);
    }

    protected override void OnDespawned(HiddenObjectView item)
    {
        base.OnDespawned(item);
        item.gameObject.SetActive(false);
    }

    protected override void OnSpawned(HiddenObjectView item)
    {
        base.OnSpawned(item);
        item.gameObject.SetActive(true);
    }
}

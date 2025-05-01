using System.Collections;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class HiddenObjectView : MonoBehaviour, IView<HiddenObjectViewModel>
{
    private HiddenObjectViewModel _viewModel;

    private CompositeDisposable _disposables = new();
    
    [SerializeField] private Image _mainImage;

    [SerializeField] private Outline _highlight;
    
    private AsyncOperationHandle<Sprite> _spriteHandle;

    private TextMeshProUGUI _displayName;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Init(HiddenObjectViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.HighlightCommand.Subscribe(_ => Highlight()).AddTo(_disposables);
        _viewModel.RevealCommand.Subscribe(_ => ForceReveal()).AddTo(_disposables);

        StartCoroutine(InitView());

        viewModel.IsFound
            .Subscribe(OnFoundChanged)
            .AddTo(_disposables);
    }

    public IEnumerator InitView()
    {
        _spriteHandle = Addressables.LoadAssetAsync<Sprite>(_viewModel.Model.AssetAdress);
        yield return _spriteHandle;

        if(_spriteHandle.Status == AsyncOperationStatus.Succeeded)
        {
            _mainImage.sprite = _spriteHandle.Result;
        }
    }

    public void InitText(TextMeshProUGUI displayName)
    {
        _displayName = displayName;
        _displayName.text = _viewModel.Model.DisplayName;
    }

    public void DespawnText()
    {
        Destroy(_displayName.gameObject);
    }

    public void OnFoundChanged(bool isFound)
    {
        if(_displayName != null)
        {
            _displayName.fontStyle = isFound ? FontStyles.Strikethrough : FontStyles.Normal;
        }
        
        if(isFound)
        {
            StartCoroutine(FoundAnimation());
        }
    }

    public void Highlight()
    {
        _highlight.enabled = true;
    }

    public void ForceReveal()
    {
        StartCoroutine(RevealAnimation());
    }

    private IEnumerator RevealAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOScale(0.8f, 0.1f).SetEase(Ease.OutQuad));
        sequence.Append(_rectTransform.DOScale(1.2f, 0.1f).SetEase(Ease.OutQuad));
        sequence.Append(_rectTransform.DOScale(0.8f, 0.1f).SetEase(Ease.OutQuad));
        sequence.Append(_rectTransform.DOScale(1.2f, 0.1f).SetEase(Ease.OutQuad));
        sequence.Append(_rectTransform.DOScale(1f, 0.1f).SetEase(Ease.OutQuad));
        yield return sequence.WaitForCompletion();
    }

    private IEnumerator FoundAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.2f, 0.1f));
        sequence.Append(transform.DOScale(1f, 0.1f));
        yield return sequence.WaitForCompletion();
    }

    public void Reset()
    {
        StopAllCoroutines();
        _viewModel.Reset();
        _disposables?.Dispose();
        _disposables = new();
    }

    public void OnClick()
    {   
        _highlight.enabled = false;
        _viewModel.MarkAsFound();
    }

    private void OnDestroy()
    {
        _disposables?.Dispose();
    }
}

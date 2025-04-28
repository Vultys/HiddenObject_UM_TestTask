using System.Collections;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class HiddenObjectView : MonoBehaviour, IView<HiddenObjectViewModel>
{
    private HiddenObjectViewModel _viewModel;

    private CompositeDisposable _disposables = new();
    
    [SerializeField] private Image _mainImage;

    [SerializeField] private Outline _foundHighlight;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Init(HiddenObjectViewModel viewModel)
    {
        StartCoroutine(viewModel.InitView(_mainImage));

        _viewModel = viewModel;

        _viewModel.IsFound
            .Subscribe(OnFoundChanged)
            .AddTo(_disposables);
    }

    private IEnumerator HighlightAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOScale(0.8f, 0.2f).SetEase(Ease.OutQuad));
        sequence.Append(_rectTransform.DOScale(1.2f, 0.2f).SetEase(Ease.OutQuad));
        sequence.Append(_rectTransform.DOScale(1f, 0.1f).SetEase(Ease.OutQuad));
        yield return sequence.WaitForCompletion();
    }


    private void OnFoundChanged(bool isFound)
    {
        if(_foundHighlight != null)
        {
            _foundHighlight.enabled = isFound;
            if(isFound)
            {
                StartCoroutine(HighlightAnimation());
            }
        }
    }

    public void OnClick()
    {   
        _viewModel.MarkAsFound();
    }

    private void OnDestroy()
    {
        _disposables?.Dispose();
    }
}

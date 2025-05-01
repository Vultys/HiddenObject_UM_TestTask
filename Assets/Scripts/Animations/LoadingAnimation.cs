using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{
    [Header("Loading Text Settings")]
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private float updateInterval = 0.5f;

    private Coroutine _animationCoroutine;

    private void OnEnable()
    {
        StartAnimation();
    }

    private void OnDisable()
    {
        StopAnimation();
    }

    /// <summary>
    /// Starts the loading animation.
    /// </summary>
    private void StartAnimation()
    {
        if(_animationCoroutine == null)
        {
            _animationCoroutine = StartCoroutine(AnimateLoadingText());
        }
    }

    /// <summary>
    /// Stops the loading animation.
    /// </summary>
    private void StopAnimation()
    {
        if(_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
            _animationCoroutine = null;
        }
    }

    /// <summary>
    /// Animates the loading text by adding dots to it.
    /// </summary>
    /// <returns> The animation coroutine </returns>
    private IEnumerator AnimateLoadingText()
    {
        int dotCount = 0;

        while(true)
        {
            _loadingText.text =  $"Loading{new string('.', dotCount)}";
            dotCount = (dotCount + 1) % 4;
            yield return new WaitForSeconds(updateInterval);
        }
    }
}

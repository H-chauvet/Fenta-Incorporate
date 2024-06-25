using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInOut : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2;
    
    private CanvasGroup _canvasGroup;
    
    private void Awake() => _canvasGroup = GetComponent<CanvasGroup>();

    public void FadeIn(Action afterCoroutine = null) => StartCoroutine(FadeCanvasGroup(_canvasGroup, 1, 0, fadeDuration, afterCoroutine));
    public void FadeOut(Action afterCoroutine = null) => StartCoroutine(FadeCanvasGroup(_canvasGroup, 0, 1, fadeDuration, afterCoroutine));

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float start, float end, float duration, Action afterCoroutine)
    {
        canvasGroup.alpha = start;
        
        float elapsedTime = 0;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }

        canvasGroup.alpha = end;
        afterCoroutine?.Invoke();
    }
}

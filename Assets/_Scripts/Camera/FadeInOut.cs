using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInOut : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2;
    [SerializeField] private float delay = 2;
    
    private CanvasGroup _canvasGroup;
    
    private void Awake() => _canvasGroup = GetComponent<CanvasGroup>();

    public void FadeIn() => StartCoroutine(FadeCanvasGroup(_canvasGroup, 1, 0, fadeDuration));
    public void FadeOut() => StartCoroutine(FadeCanvasGroup(_canvasGroup, 0, 1, fadeDuration));

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float start, float end, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        canvasGroup.alpha = start;
        
        float elapsedTime2 = 0;
        
        while (elapsedTime2 < fadeDuration)
        {
            elapsedTime2 += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }

        canvasGroup.alpha = end;
    }
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInOut : MonoBehaviour
{
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private float fadeDuration = 2;
    
    private CanvasGroup _canvasGroup;
    
    private void Awake() => _canvasGroup = GetComponent<CanvasGroup>();

    private void Start()
    {
        if (fadeIn)
            FadeIn();
        else
            FadeOut();
    }

    public void FadeIn() => StartCoroutine(FadeCanvasGroup(_canvasGroup, 1, 0, fadeDuration));
    public void FadeOut() => StartCoroutine(FadeCanvasGroup(_canvasGroup, 0, 1, fadeDuration));

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float start, float end, float duration)
    {
        canvasGroup.alpha = start;
        
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }

        canvasGroup.alpha = end;
    }
}

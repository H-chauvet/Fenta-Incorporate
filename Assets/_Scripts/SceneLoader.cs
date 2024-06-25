using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private FadeInOut _fadeInOut;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        _fadeInOut.FadeIn();
    }

    private void Awake()
    {
        _fadeInOut = GetComponentInChildren<FadeInOut>();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        _fadeInOut.FadeOut(() => SceneManager.LoadScene(sceneName));
    }
}

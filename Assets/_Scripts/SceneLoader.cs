using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private FadeInOut _fadeInOut;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _fadeInOut = GetComponentInChildren<FadeInOut>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        _fadeInOut.FadeIn();
    }

    public void LoadScene(string sceneName)
    {
        _fadeInOut.FadeOut(() => SceneManager.LoadScene(sceneName));
    }
}

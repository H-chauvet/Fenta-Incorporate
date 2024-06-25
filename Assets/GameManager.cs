using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void LoadScene(string sceneName)
    {
        _sceneLoader.LoadScene(sceneName);
    }
}

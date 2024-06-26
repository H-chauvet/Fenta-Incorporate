using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject HUD;

    private InputAction _pauseAction;
    private bool _isGamePaused;

    private void Awake()
    {
        _pauseAction = InputSystem.actions.FindAction("Pause");
    }

    private void Update()
    {
        if (_pauseAction.WasPressedThisFrame())
        {
            Pause(_isGamePaused);
        }
    }

    public void Pause(bool isGamePaused)
    {
        if (isGamePaused)
        {
            Time.timeScale = 1;
            pauseMenuCanvas.SetActive(false);
            HUD.SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            pauseMenuCanvas.SetActive(true);
            HUD.SetActive(false);
        }
    }
}

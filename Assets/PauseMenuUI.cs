using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    
    private InputAction _homeAction;
    private InputAction _continueAction;
    private InputAction _restartAction;

    private void OnEnable()
    {
        _homeAction = InputSystem.actions.FindAction("Home");
        _continueAction = InputSystem.actions.FindAction("Continue");
        _restartAction = InputSystem.actions.FindAction("Restart");
    }

    private void Update()
    {
        if (_homeAction.WasPressedThisFrame()) SceneManager.LoadScene("Main Menu");
        if (_continueAction.WasPressedThisFrame()) uiController.Pause(false);
        if (_restartAction.WasPressedThisFrame()) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

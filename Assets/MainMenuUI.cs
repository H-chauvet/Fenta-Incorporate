using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private SceneLoader SceneLoader;
    
    private InputAction mainUIAction;

    private void OnEnable()
    {
        mainUIAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        if (mainUIAction.WasPressedThisFrame())
        {
            SceneLoader.LoadScene("00 - Room");
        }
    }
}

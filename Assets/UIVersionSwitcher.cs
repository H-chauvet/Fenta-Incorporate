using UnityEngine;
using UnityEngine.InputSystem;

public class UIVersionSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] UIVersions = new GameObject[2];
    private InputAction UISwitchAction;

    private void Awake()
    {
        UISwitchAction = InputSystem.actions.FindAction("UISwitch");
    }

    private void Update()
    {
        if (UISwitchAction.WasPressedThisFrame())
        {
            bool state = UIVersions[0].activeSelf;
            UIVersions[0].SetActive(!state);
            UIVersions[1].SetActive(state);
        }
    }
}

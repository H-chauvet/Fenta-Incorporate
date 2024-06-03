using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class MonsterEventInteraction : MonoBehaviour, IInteractable
{

    private InputAction basicInteractionButton;
    private InputAction mainInteractionButton;
    private InputAction secondaryInteractionButton;

    [SerializeField] private UnityEvent _basicInteractEvent;
    [SerializeField] private UnityEvent _mainInteractEvent;
    [SerializeField] private UnityEvent _secondaryInteractEvent;

    // Start is called before the first frame update
    void Start()
    {
        basicInteractionButton = InputSystem.actions.FindAction("Interact");
        mainInteractionButton = InputSystem.actions.FindAction("Interact");
        secondaryInteractionButton = InputSystem.actions.FindAction("Interact");
    }

    public void Interact()
    {
        if (basicInteractionButton.IsPressed())
        {
            onBasicInteractEvent();
        }
        if (mainInteractionButton.IsPressed())
        {
            onMainInteractEvent();
        }
        if (secondaryInteractionButton.IsPressed())
        {
            onSecondaryInteractEvent();
        }
    }

    public void onBasicInteractEvent()
    {
        _basicInteractEvent.Invoke();
    }

    public void onMainInteractEvent()
    {
        _mainInteractEvent.Invoke();
    }

    public void onSecondaryInteractEvent()
    {
        _secondaryInteractEvent.Invoke();
    }
}

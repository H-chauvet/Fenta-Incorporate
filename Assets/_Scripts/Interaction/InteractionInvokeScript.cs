using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

// public class enum MonsterType
// {
//     All,
//     Bloetje = "Character1",
//     Hijsi = "Character2",
//     Robotmonster = "Character3",
//     Elisa = "Character4"
// }

public class InteractionInvokeScript : MonoBehaviour, IInteractable
{

    private InputAction basicInteractionButton;
    private InputAction mainInteractionButton;
    private InputAction secondaryInteractionButton;

    // public MonsterType interactionMonster = MonsterType.All;


    [SerializeField] private UnityEvent _basicInteractEvent;
    [SerializeField] private UnityEvent _mainInteractEvent;
    [SerializeField] private UnityEvent _secondaryInteractEvent;



    // Start is called before the first frame update
    void Start()
    {
        basicInteractionButton = InputSystem.actions.FindAction("Interact");
        mainInteractionButton = InputSystem.actions.FindAction("MainInteract");
        secondaryInteractionButton = InputSystem.actions.FindAction("SecondaryInteract");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        // if (interactionMonster == MonsterType.All) {
        Interact(other.gameObject);
        // } 
        // else if (other.gameObject.tag == interactionMonster.ToString())
        // {
            Interact(other.gameObject);
        // }
    }

    public void Interact(GameObject obj)
    {
        
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

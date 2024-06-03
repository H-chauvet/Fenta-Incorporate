using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ObjectInteraction : MonoBehaviour, IInteractable
{

    private InputAction basicInteractionButton;
    private InputAction mainInteractionButton;
    private InputAction secondaryInteractionButton;

    public MonsterType interactionMonster = MonsterType.All;


    [SerializeField] private UnityEvent _basicInteractEvent;
    [SerializeField] private UnityEvent _mainInteractEvent;
    [SerializeField] private UnityEvent _secondaryInteractEvent;

    private Dictionary<MonsterType, string> monsterTags = new Dictionary<MonsterType, string>
    {
        {MonsterType.All, "All"},
        {MonsterType.Bloetje, "Character1"},
        {MonsterType.Hijsi, "Character2"},
        {MonsterType.Robotmonster, "Character3"},
        {MonsterType.Elisa, "Character4"}
    };



    // Start is called before the first frame update
    void Start()
    {
        basicInteractionButton = InputSystem.actions.FindAction("Interact");
        mainInteractionButton = InputSystem.actions.FindAction("Interact");
        secondaryInteractionButton = InputSystem.actions.FindAction("Interact");
    }

    public void OnTriggerStay(Collider other)
    {   
        PlayerSwitchScript playerSwitchScript = other.gameObject.GetComponent<PlayerSwitchScript>();
        if (playerSwitchScript == null)
        {
            return;
        }
        GameObject children = other.gameObject.transform.GetChild(playerSwitchScript.whichCharacter).gameObject;
        Debug.Log(children.tag);
        if (interactionMonster == MonsterType.All) {
            Interact();
        } 
        else if (children.tag == monsterTags[interactionMonster])
        {
            Interact();
        }
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

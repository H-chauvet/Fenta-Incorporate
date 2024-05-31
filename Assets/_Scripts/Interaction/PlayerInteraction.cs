using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class PlayerInteraction : MonoBehaviour, IInteractable
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

    public void Update()
    {   
        PlayerSwitchScript playerSwitchScript = GetComponent<PlayerSwitchScript>();
        if (playerSwitchScript == null)
        {
            return;
        }
        GameObject children = gameObject.transform.GetChild(playerSwitchScript.whichCharacter).gameObject;
        
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
        if (basicInteractionButton.triggered)
        {
            onBasicInteractEvent();
        }
        if (mainInteractionButton.triggered)
        {
            onMainInteractEvent();
        }
        if (secondaryInteractionButton.triggered)
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

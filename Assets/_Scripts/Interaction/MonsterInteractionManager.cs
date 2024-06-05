using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class MonsterInteractionManager : MonoBehaviour
{

    private IMonsterAbilities monsterAbilities;
    private IInteractable monsterInteraction;

    private InputAction mainInteractionButton;
    private InputAction secondaryInteractionButton;

    // Start is called before the first frame update
    void Start()
    {
        mainInteractionButton = InputSystem.actions.FindAction("Interact");
        secondaryInteractionButton = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSwitchScript playerSwitchScript = GetComponent<PlayerSwitchScript>();
        if (playerSwitchScript == null)
        {
            return;
        }
        GameObject children = gameObject.transform.GetChild(playerSwitchScript.whichCharacter).gameObject;
        monsterAbilities = children.GetComponent<IMonsterAbilities>();
        if (monsterAbilities != null)
        {
            UseAbility();
        }
        monsterInteraction = children.GetComponent<IInteractable>();
        if (monsterInteraction != null)
        {
            monsterInteraction.Interact();
        }
    }

    void UseAbility()
    {
        if (mainInteractionButton.IsPressed())
        {
            MainAbilityInteraction();
        }
        if (secondaryInteractionButton.IsPressed())
        {
            SecondaryAbilityInteraction();
        }
    }

    void MainAbilityInteraction()
    {
        monsterAbilities.MainAbilityInteraction();
    }

    void SecondaryAbilityInteraction()
    {
        monsterAbilities.SecondaryAbilityInteraction();
    }
}

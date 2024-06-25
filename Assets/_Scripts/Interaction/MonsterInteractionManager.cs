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

    private PlayerAnimation _playerAnimation;

    private PlayerSwitchScript playerSwitchScript;
    private int currentCharacter = -1;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        playerSwitchScript = GetComponent<PlayerSwitchScript>();
        mainInteractionButton = InputSystem.actions.FindAction("InteractMain");
        secondaryInteractionButton = InputSystem.actions.FindAction("InteractSecond");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSwitchScript == null)
        {
            return;
        }
        if (playerSwitchScript.whichCharacter != currentCharacter)
        {
            currentCharacter = playerSwitchScript.whichCharacter;
            monsterAbilities = gameObject.transform.GetChild(playerSwitchScript.whichCharacter).GetComponent<IMonsterAbilities>();
            monsterInteraction = gameObject.transform.GetChild(playerSwitchScript.whichCharacter).GetComponent<IInteractable>();
        }
        if (monsterAbilities != null)
        {
            UseAbility();
        }
        if (monsterInteraction != null)
        {
            // monsterInteraction.Interact();
        }
    }

    void UseAbility()
    {
        if (mainInteractionButton.WasPressedThisFrame())
        {
            MainAbilityInteraction();
        }
        if (secondaryInteractionButton.WasPressedThisFrame())
        {
            SecondaryAbilityInteraction();
        }
    }

    void MainAbilityInteraction()
    {
        monsterAbilities.MainAbilityInteraction(_playerAnimation._animator);
    }

    void SecondaryAbilityInteraction()
    {
        //monsterAbilities.SecondaryAbilityInteraction(_playerAnimation._animator);
    }
}

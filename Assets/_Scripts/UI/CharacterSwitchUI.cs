using UnityEngine;

public class CharacterSwitchUI : MonoBehaviour
{
    [SerializeField] private IconSwapUI[] monsterIcons;
    
    private void Awake()
    {
        PlayerSwitchScript.CharacterSwitch += OnCharacterSwitch;
    }

    private void OnCharacterSwitch(int whichCharacter, GameObject character)
    {
        foreach (var monsterUI in monsterIcons)
        {
            monsterUI.Deactivate();
        }

        monsterIcons[whichCharacter].Activate();
    }
}

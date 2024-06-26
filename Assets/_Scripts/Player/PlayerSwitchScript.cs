using System;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwitchScript : MonoBehaviour
{
    public List<GameObject> possibleCharacters;
    public int whichCharacter;
    private Vector3 previousPosition;
    private InputAction UpMonster;
    private InputAction DownMonster;
    private InputAction RightMonster;
    private InputAction LeftMonster;
    private InputAction PreviousMonster;
    private InputAction NextMonster;
    private InputAction interactMain;


    private bool IsSwitchingPressed = false;
    private bool IsSwitchingPressed2 = false;
    private bool IsSwitchingPressed3 = false;

    //Audio
    public AudioSource abilitySource;
    public AudioSource walkingSource;
    public AudioClip monster0;
    public AudioClip monster1;
    public AudioClip monster2;
    public AudioClip monster3;
    public AudioClip monsterAbility0;
    public AudioClip monsterAbility1;
    public AudioClip monsterAbility2;
    public AudioClip monsterAbility3;
    bool abilitySoundIsPlaying = false;
    bool walkingSoundIsPlaying = false;
    public float abilitySoundDuration = 1f;
    public float walkingSoundDuration = 0.5f;


    public PlayerMovement pm;

    public static Action<int, GameObject> CharacterSwitch;
    
    void Start()
    {
        UpMonster = InputSystem.actions.FindAction("UpMonster");
        DownMonster = InputSystem.actions.FindAction("DownMonster");
        LeftMonster = InputSystem.actions.FindAction("LeftMonster");
        RightMonster = InputSystem.actions.FindAction("RightMonster");
        PreviousMonster = InputSystem.actions.FindAction("PreviousMonster");
        NextMonster = InputSystem.actions.FindAction("NextMonster");
        interactMain = InputSystem.actions.FindAction("InteractMain");
        previousPosition = possibleCharacters[whichCharacter].transform.position;
        SwitchCharacter(0);

        
        
    }

    void EnableAbilitySound()
    {
        abilitySoundIsPlaying = false;
    }

    void EnableWalkingSound()
    {
        walkingSoundIsPlaying = false;
    }

    void Update()
    {
        //Ability Sounds
        if(!abilitySoundIsPlaying && interactMain.WasPressedThisFrame())
        {

            if(whichCharacter == 0)
            {
                abilitySoundIsPlaying = true;
                abilitySource.PlayOneShot(monsterAbility0);
                Invoke("EnableAbilitySound",monsterAbility0.length);
            }

            if(whichCharacter == 1)
            {
                abilitySoundIsPlaying = true;
                abilitySource.PlayOneShot(monsterAbility1);
                Invoke("EnableAbilitySound",monsterAbility1.length);
            }

            if(whichCharacter == 2)
            {
                abilitySoundIsPlaying = true;
                abilitySource.PlayOneShot(monsterAbility2);
                Invoke("EnableAbilitySound",monsterAbility2.length);
            }

            if(whichCharacter == 3)
            {
                abilitySoundIsPlaying = true;
                abilitySource.PlayOneShot(monsterAbility3);
                Invoke("EnableAbilitySound",monsterAbility3.length);
            }

        }
        //Walking Sounds
        if(!walkingSoundIsPlaying && (pm.horizontalInput!=0 || pm.verticalInput!=0))
        {
            if(whichCharacter == 0)
            {
                walkingSoundIsPlaying = true;
                walkingSource.PlayOneShot(monster0);
                Invoke("EnableWalkingSound",monster0.length);
            } 
            if(whichCharacter == 1)
            {
                walkingSoundIsPlaying = true;
                walkingSource.PlayOneShot(monster1);
                Invoke("EnableWalkingSound",monster1.length);
            }

            if(whichCharacter == 2)
            {
                walkingSoundIsPlaying = true;
                walkingSource.PlayOneShot(monster2);
                Invoke("EnableWalkingSound",monster2.length);
            }

            if(whichCharacter == 3)
            {
                walkingSoundIsPlaying = true;
                walkingSource.PlayOneShot(monster3);
                Invoke("EnableWalkingSound",monster3.length);
            }   
        }

        float dPadHorizontal = Input.GetAxis("DPadHorizontal");
        float dPadVertical = Input.GetAxis("DPadVertical");

        if (!IsSwitchingPressed)
        {
            
            if (NextMonster.IsPressed())
            {
                IsSwitchingPressed = true;
                if (whichCharacter == 3)
                {
                    SwitchCharacter(0);
                }
                else
                {
                    SwitchCharacter(whichCharacter, 1);
                }
            }
        } else if (!NextMonster.IsPressed())
        {
            IsSwitchingPressed = false;
        }

        if (!IsSwitchingPressed2)
        {
            if (PreviousMonster.IsPressed())
            {
                IsSwitchingPressed2 = true;
                if (whichCharacter == 0)
                {
                    SwitchCharacter(3);
                }
                else
                {
                    SwitchCharacter(whichCharacter, -1);
                }
            }
        }
        else if (!PreviousMonster.IsPressed())
        {
            IsSwitchingPressed2 = false;
        }

        if (!IsSwitchingPressed3)
        {
            if (UpMonster.IsPressed()) // D-Pad Up
            {
                IsSwitchingPressed3 = true;
                SwitchCharacter(0);

            }
            else if (RightMonster.IsPressed()) // D-Pad Right
            {
                IsSwitchingPressed3 = true;
                SwitchCharacter(1);

            }
            else if (DownMonster.IsPressed()) // D-Pad Down
            {
                IsSwitchingPressed3 = true;
                SwitchCharacter(2);

            }
            else if (LeftMonster.IsPressed()) // D-Pad Left
            {
                IsSwitchingPressed3 = true;
                SwitchCharacter(3);
            }
        }
        else
        {
            if (!UpMonster.IsPressed() && !RightMonster.IsPressed() && !DownMonster.IsPressed() && !LeftMonster.IsPressed())
            {
                IsSwitchingPressed3 = false;
            }
        }

    }

    private void SwitchCharacter(int characterIndex, int changeIndex = 0)
    {
        previousPosition = possibleCharacters[whichCharacter].transform.position;
        whichCharacter = characterIndex + changeIndex;
        CharacterSwitch.Invoke(whichCharacter, possibleCharacters[whichCharacter]);
        Swap();
    
    }

    public void Swap()
    {
        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            if (i == whichCharacter)
            {
                possibleCharacters[i].transform.position = previousPosition;
                possibleCharacters[i].SetActive(true);
            }
            else
            {
                possibleCharacters[i].SetActive(false);
            }
        }
    }
}

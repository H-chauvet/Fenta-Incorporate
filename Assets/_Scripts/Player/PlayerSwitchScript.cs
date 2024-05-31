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


    private bool IsSwitchingPressed = false;
    private bool IsSwitchingPressed2 = false;
    private bool IsSwitchingPressed3 = false;

    public static Action<int, GameObject> CharacterSwitch;
    
    void Start()
    {
        UpMonster = InputSystem.actions.FindAction("UpMonster");
        DownMonster = InputSystem.actions.FindAction("DownMonster");
        LeftMonster = InputSystem.actions.FindAction("LeftMonster");
        RightMonster = InputSystem.actions.FindAction("RightMonster");
        PreviousMonster = InputSystem.actions.FindAction("PreviousMonster");
        NextMonster = InputSystem.actions.FindAction("NextMonster");
        previousPosition = possibleCharacters[whichCharacter].transform.position;
        SwitchCharacter(0);
    }

    void Update()
    {
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

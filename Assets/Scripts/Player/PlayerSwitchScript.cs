using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwitchScript : MonoBehaviour
{
    public List<GameObject> possibleCharacters;
    public CinemachineVirtualCamera virtualCamera;
    public int whichCharacter;
    private Vector3 previousPosition;
    private bool canSwitch = true; // Flag to ensure single switch per press
    private InputAction UpMonster;
    private InputAction DownMonster;
    private InputAction RightMonster;
    private InputAction LeftMonster;

    void Start()
    {
        UpMonster = InputSystem.actions.FindAction("UpMonster");
        DownMonster = InputSystem.actions.FindAction("DownMonster");
        LeftMonster = InputSystem.actions.FindAction("LeftMonster");
        RightMonster = InputSystem.actions.FindAction("RightMonster");
        previousPosition = possibleCharacters[whichCharacter].transform.position;
        Swap();
    }

    void Update()
    {
        float dPadHorizontal = Input.GetAxis("DPadHorizontal");
        float dPadVertical = Input.GetAxis("DPadVertical");

        if (canSwitch)
        {
            if (UpMonster.IsPressed()) // D-Pad Up
            {
                SwitchCharacter(0);
            }
            else if (RightMonster.IsPressed()) // D-Pad Right
            {
                SwitchCharacter(1);
            }
            else if (DownMonster.IsPressed()) // D-Pad Down
            {
                SwitchCharacter(2);
            }
            else if (LeftMonster.IsPressed()) // D-Pad Left
            {
                SwitchCharacter(3);
            }

            if (Mathf.Abs(dPadHorizontal) > 0.5f || Mathf.Abs(dPadVertical) > 0.5f)
            {
                canSwitch = false;
            }
        }
        else
        {
            if (Mathf.Abs(dPadHorizontal) < 0.5f && Mathf.Abs(dPadVertical) < 0.5f)
            {
                canSwitch = true;
            }
        }
    }

    private void SwitchCharacter(int characterIndex)
    {
        previousPosition = possibleCharacters[whichCharacter].transform.position;
        whichCharacter = characterIndex;
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

                virtualCamera.Follow = possibleCharacters[i].transform;
                virtualCamera.LookAt = possibleCharacters[i].transform;
            }
            else
            {
                possibleCharacters[i].SetActive(false);
            }
        }
    }
}

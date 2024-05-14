using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchScript : MonoBehaviour
{
    public List<GameObject> possibleCharacters;
    public CinemachineVirtualCamera virtualCamera;
    public int whichCharacter;
    private Vector3 previousPosition;
    private bool canSwitch = true; // Flag to ensure single switch per press

    void Start()
    {
        previousPosition = possibleCharacters[whichCharacter].transform.position;
        Swap();
    }

    void Update()
    {
        float dPadHorizontal = Input.GetAxis("DPadHorizontal");
        float dPadVertical = Input.GetAxis("DPadVertical");

        if (canSwitch)
        {
            if (dPadVertical > 0.5f) // D-Pad Up
            {
                SwitchCharacter(0);
            }
            else if (dPadHorizontal > 0.5f) // D-Pad Right
            {
                SwitchCharacter(1);
            }
            else if (dPadVertical < -0.5f) // D-Pad Down
            {
                SwitchCharacter(2);
            }
            else if (dPadHorizontal < -0.5f) // D-Pad Left
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

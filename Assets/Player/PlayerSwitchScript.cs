using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchScript : MonoBehaviour
{
    public List<GameObject> possibleCharacters;
    public int whichCharacter;
    private Vector3 previousPosition;

    void Start()
    {
        Swap();
    }

    void Update()
    {
        if (Input.GetKeyDown("[-]"))
        {
            if (whichCharacter == 0)
            {
                whichCharacter = possibleCharacters.Count - 1;
            }
            else
            {
                whichCharacter -= 1;
            }
            Swap();
        }
        if (Input.GetKeyDown("[+]"))
        {
            Debug.Log("Previous position will be : " + previousPosition);
            if (whichCharacter == possibleCharacters.Count - 1)
            {
                whichCharacter = 0;
            }
            else
            {
                whichCharacter += 1;
            }
            Swap();
        }
    }

    public void Swap()
    {
        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            if (i == whichCharacter)
            {
                if (previousPosition != Vector3.zero)
                {
                    possibleCharacters[i].transform.position = previousPosition;
                }
                possibleCharacters[i].SetActive(true);
            }
            else
            {
                previousPosition = possibleCharacters[i].transform.position;
                possibleCharacters[i].SetActive(false);
                
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSwitchScript : MonoBehaviour
{

    public Transform character;
    public List<Transform> possibleCharacters;
    public int whichCharacter;

    // Start is called before the first frame update
    void Start()
    {
        if (character == null && possibleCharacters.Count >= 1)
        {
            character = possibleCharacters[0];
        }
        Swap();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(possibleCharacters.Count);
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
        character = possibleCharacters[whichCharacter];
        character.GetComponent<PlayerMovement>().enabled = true;
        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            if (possibleCharacters[i] != character)
            {
                possibleCharacters[i].GetComponent<PlayerMovement>().enabled = false;
            }
        }
    }
}

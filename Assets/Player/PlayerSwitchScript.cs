using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSwitchScript : MonoBehaviour
{

    public List<GameObject> possibleCharacters;
    public int whichCharacter;

    // Start is called before the first frame update
    void Start()
    {
        Swap();
    }

    // Update is called once per frame
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
                possibleCharacters[i].SetActive(true);
            }
            else
            {
                possibleCharacters[i].SetActive(false);
            }
        }
    }
}

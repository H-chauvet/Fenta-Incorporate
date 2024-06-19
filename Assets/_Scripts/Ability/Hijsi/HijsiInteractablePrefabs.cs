/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HijsiSpecialDash : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject hijsi;
    private HijsiAbilities abilities;

    void Start()
    {
        abilities = hijsi.GetComponent<HijsiAbilities>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        abilities.isDashedControlled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        abilities.isDashedControlled = false;
    }
}
*/
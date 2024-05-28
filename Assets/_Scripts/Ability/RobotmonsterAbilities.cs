using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotmonsterAbilities : MonoBehaviour
{

    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    public void LightningPower(GameObject newLocation)
    {
        parent.transform.position = newLocation.transform.position;
    }
    public void WaterPower()
    {
        Debug.Log("Water");
    }
}

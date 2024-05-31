using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotmonsterAbilities : MonoBehaviour
{

    public float lightningPowerCooldown = 5f;
    public float waterPowerCooldown = 5f;

    private float lightningPowerDuration = 5f;
    private float waterPowerDuration = 5f;

    private bool lightningPowerReady = true;
    private bool waterPowerReady = true;

    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    void Update()
    {
        if (lightningPowerReady == false)
        {
            lightningPowerDuration -= Time.deltaTime;
            if (lightningPowerDuration <= 0)
            {
                Debug.Log("Lightning");
                lightningPowerReady = true;
                lightningPowerDuration = lightningPowerCooldown;
            }
        }
        if (waterPowerReady == false)
        {
            waterPowerDuration -= Time.deltaTime;
            if (waterPowerDuration <= 0)
            {
                waterPowerReady = true;
                waterPowerDuration = waterPowerCooldown;
            }
        }
    }

    public void LightningPower(GameObject newLocation)
    {
        if (lightningPowerReady == false)
        {
            return;
        }
        lightningPowerReady = false;
        parent.transform.position = newLocation.transform.position;
        
    }
    public void WaterPower()
    {
        if (waterPowerReady == false)
        {
            return;
        }
        waterPowerReady = false;
        Debug.Log("Water");
    }
}

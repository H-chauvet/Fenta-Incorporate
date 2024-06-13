using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterTimer : MonoBehaviour
{
    public float timer = 5f;
    private float currentTime = 0f;
    [HideInInspector] public bool canTeleport = false;

    // Update is called once per frame
    void Update()
    {
        if (canTeleport == false)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                canTeleport = true;
            }
        }
    }

    public void StartTimer()
    {
        currentTime = timer;
        canTeleport = false;
    }
}

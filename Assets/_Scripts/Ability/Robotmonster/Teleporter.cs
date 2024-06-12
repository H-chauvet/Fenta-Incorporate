using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportTarget;
    public TeleporterTimer timer;

    private Transform player;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            player = other.transform;
    }

    public void Teleport()
    {
        if (timer.canTeleport == false)
            return;
        player.position = teleportTarget.position;
        timer.StartTimer();
    }

}

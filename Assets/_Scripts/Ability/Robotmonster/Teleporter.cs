using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject teleportationVFX;
    public float teleportationVFXDuration = 1f; // Duration for the VFX
    public float teleportationSpeed = 10f;
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
        
        timer.StartTimer();
        StartCoroutine(SpawnVFX());
        player.position = teleportTarget.position;
    }

    public IEnumerator SpawnVFX()
    {
        GameObject vfxInstance = Instantiate(teleportationVFX, player.position, Quaternion.identity);
        float elapsedTime = 0f;
        while (elapsedTime < teleportationVFXDuration)
        {
            float t = elapsedTime / teleportationVFXDuration;
            vfxInstance.transform.position = Vector3.Lerp(player.position, teleportTarget.position, t);
            elapsedTime += Time.deltaTime * teleportationSpeed;
            yield return null;
        }
        
        vfxInstance.transform.position = teleportTarget.position;

        // Destroy the VFX object after the duration
        Destroy(vfxInstance, teleportationVFXDuration - elapsedTime);
    }

}

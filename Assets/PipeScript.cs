using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public GameObject spherePrefab;
    public float launchForce = 10f;
    public Transform spawnPoint;
    public float spawnInterval = 0.5f; // Time interval between sphere spawns

    private bool isSpawning = false;
    private Coroutine spawnCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isSpawning)
            {
                StopSpawning();
            }
            else
            {
                StartSpawning();
            }
        }
    }

    private void StartSpawning()
    {
        isSpawning = true;
        spawnCoroutine = StartCoroutine(SpawnSpheres());
    }

    private void StopSpawning()
    {
        isSpawning = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    private IEnumerator SpawnSpheres()
    {
        while (isSpawning)
        {
            SpawnSphere();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnSphere()
    {
        GameObject sphere = Instantiate(spherePrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = sphere.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.right * launchForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSpawning = false; // Ensure it doesn't start spawning immediately
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopSpawning();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeFaucet : MonoBehaviour
{
    public GameObject prefabToSpawn;   // The prefab to spawn
    public float spawnInterval = 0.1f; // Time interval between spawns

    private bool isSpawning = false;
    private Coroutine spawnCoroutine;

    void Update()
    {
        // Check for E key press to toggle spawning
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

    void StartSpawning()
    {
        isSpawning = true;
        spawnCoroutine = StartCoroutine(SpawnPrefabs());
    }

    void StopSpawning()
    {
        isSpawning = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    IEnumerator SpawnPrefabs()
    {
        while (isSpawning)
        {
            for (int i = 0; i < 100; i++)
            {
                GameObject spawnedPrefab = Instantiate(prefabToSpawn, transform.position, transform.rotation);
                Destroy(spawnedPrefab, 5.0f);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

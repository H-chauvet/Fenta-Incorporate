using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloetjePower : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval;
    private float currentSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnTime = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        InstantiateBlood();
    }

    private void InstantiateBlood() {
        currentSpawnTime -= Time.deltaTime;
        if (spawnInterval == 0) {
            return;
        }
        if (currentSpawnTime <= 0)
        {
            currentSpawnTime = spawnInterval;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                Vector3 spawnPosition = hit.point;
                spawnPosition.y += 0.01f;
                Instantiate(objectToSpawn, spawnPosition, Quaternion.Euler(90, 0, 0));
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class OrbitingObjects : MonoBehaviour
{
    // Les objets qui tourneront autour du GameObject principal
    public List<GameObject> orbitingPrefabs;

    // La vitesse angulaire de rotation
    public float rotationSpeed = 50.0f;

    // La distance d'orbite
    public float orbitDistance = 5.0f;

    public float particleNumber = 50;

    private Dictionary<GameObject, float> particles = new Dictionary<GameObject, float>();

    void Start()
    {
        for (int i = 0; i < particleNumber; i++)
        {
            int random = Random.Range(0, orbitingPrefabs.Count);
            Vector3 randomOffset = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            Vector3 randomRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            float rotationSpeed = Random.Range(50f, 250f);
            GameObject particle = Instantiate(orbitingPrefabs[random], transform);
            if (particle == null)
            {
                Debug.Log("Couldnt create particle");
                continue;
            }
            particle.transform.position = transform.position + new Vector3(randomOffset.x * 0.5f, randomOffset.y * 1.5f, randomOffset.z * 0.5f);
            particle.transform.rotation = Quaternion.Euler(randomRotation);
            particles.Add(particle, rotationSpeed);
        }

        Debug.Log(particles.Count);


        /*for (int i = 0; i < orbitingPrefabs.Count; i++)
        {
            float angle = i * Mathf.PI * 2 / orbitingPrefabs.Count;
            Vector3 newPosition = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * orbitDistance;
            orbitingPrefabs[i].transform.position = transform.position + newPosition;
        }*/
    }

    void Update()
    {
        foreach (var particle in particles)
        {
            particle.Key.transform.RotateAround(transform.position, Vector3.up, particle.Value * Time.deltaTime);
        }

        /*for (int i = 0; i < orbitingPrefabs.Count; i++)
        {
            orbitingPrefabs[i].transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }*/
    }
}

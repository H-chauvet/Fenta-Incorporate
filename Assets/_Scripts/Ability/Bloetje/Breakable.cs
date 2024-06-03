using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public List<GameObject> debris;
    public int minDebris = 5;
    public int maxDebris = 10;
    public float positionOffset = 0.5f;
    public float explosionMinForce = 1f;
    public float explosionMaxForce = 2f;

    public float debrisVolumeFromParent = 0.8f;
    public float debrisMinSize = 0.8f;
    public float debrisMaxSize = 1.2f;

    public void Break()
    {
        int debrisAmount = Random.Range(minDebris, maxDebris);
        
        for (int i = 0; i < debrisAmount; i++)
        {
            int randomDebris = Random.Range(0, debris.Count);
            Vector3 randomRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

            Vector3 randomOffset = new Vector3(Random.Range(-transform.localScale.x * positionOffset, transform.localScale.x * positionOffset),
                                               Random.Range(-transform.localScale.y * positionOffset, transform.localScale.y * positionOffset),
                                               Random.Range(-transform.localScale.z * positionOffset, transform.localScale.z * positionOffset));
            Vector3 debrisPosition = transform.position + randomOffset;
            GameObject debrisInstance = Instantiate(debris[randomDebris], debrisPosition, Quaternion.Euler(randomRotation));
            Rigidbody rb = debrisInstance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float randomSize = Random.Range(debrisMinSize, debrisMaxSize);

                float originalVolume = transform.localScale.x * transform.localScale.y * transform.localScale.z;
                float totalDebrisVolume = originalVolume * debrisVolumeFromParent;
                float debrisVolume = totalDebrisVolume / debrisAmount * randomSize;
                float debrisScale = Mathf.Pow(debrisVolume, 1f / 3f);
                rb.transform.localScale = new Vector3(debrisScale, debrisScale, debrisScale); 


                float force = Random.Range(explosionMinForce, explosionMaxForce);
                Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                Debug.Log(direction);
                rb.AddForce(direction * force * rb.transform.localScale.y, ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }
}

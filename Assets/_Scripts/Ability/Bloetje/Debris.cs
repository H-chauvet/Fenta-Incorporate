using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public float lifeTimeMin = 1f;
    public float lifeTimeMax = 3f;

    // Start is called before the first frame update
    void Start()
    {
        float lifeTime = Random.Range(lifeTimeMin, lifeTimeMax);
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.GetComponent<Debris>() != null)
        {
            return;
        }
        Destroy(gameObject);
    }
}

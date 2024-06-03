using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class WaterProjectile : MonoBehaviour
{
    public float lifeTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.GetComponent<WaterProjectile>() != null)
        {
            return;
        }
        
        // Check if the collided object has the Target component
        WaterTarget target = other.gameObject.GetComponent<WaterTarget>();
        if (target != null)
        {
            target.OnHit();
        }

        // Destroy the projectile upon collision
        Destroy(gameObject);
    }
}

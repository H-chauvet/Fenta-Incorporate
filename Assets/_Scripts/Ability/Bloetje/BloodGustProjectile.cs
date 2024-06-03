using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class BloodGustProjectile : MonoBehaviour
{
    public float lifeTime = 1f;
    public float strength = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.GetComponent<BloodGustProjectile>() != null)
        {
            return;
        }
        
        // Check if the collided object has the Target component
        BloodGustTarget target = other.gameObject.GetComponent<BloodGustTarget>();
        if (target != null)
        {
            target.OnHit();
            Rigidbody targetRb = other.gameObject.GetComponent<Rigidbody>();
            if (targetRb != null)
            {
                Rigidbody projectileRb = transform.GetComponent<Rigidbody>();
                Vector3 direction = projectileRb ? projectileRb.velocity.normalized : Vector3.zero;
                targetRb.AddForce(direction * 2000 * strength, ForceMode.Impulse);
            }
        }
        
        // Destroy the projectile upon collision
        Destroy(gameObject);
    }
}

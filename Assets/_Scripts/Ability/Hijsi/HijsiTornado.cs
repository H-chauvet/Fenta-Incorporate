using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HijsiTornado : MonoBehaviour
{
    public float lifeTime = 5f;
    public float liftForce = 1f;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }
        rb = other.gameObject.GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rb.AddForce(Vector3.up * liftForce * 10, ForceMode.Acceleration);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExtruder : MonoBehaviour
{
    public float extrusionHeight = 1f;
    public float maxExtrusion = 5f;
    private float currentExtrusion = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sphere") && currentExtrusion < maxExtrusion)
        {
            Extrude();
        }
    }

    private void Extrude()
    {
        float extrusionAmount = Mathf.Min(extrusionHeight, maxExtrusion - currentExtrusion);
        transform.localScale += new Vector3(0, extrusionAmount, 0);
        currentExtrusion += extrusionAmount;
    }

}

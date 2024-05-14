using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScript : MonoBehaviour
{

    public float timeToGrow = 0.5f;
    public float timeToWait = 2f;
    public float timeToShrink = 5f;
    
    private float elapsedTime = 0f;
    private int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0) {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsedTime / timeToGrow);
            if (elapsedTime >= timeToGrow) {
                elapsedTime = 0f;
                transform.localScale = Vector3.one;
                state = 1;
            }
        } else if (state == 1) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeToWait) {
                elapsedTime = 0f;
                state = 2;
            }
        } else if (state == 2) {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, elapsedTime / timeToShrink);
            if (elapsedTime >= timeToShrink) {
                Destroy(gameObject);
            }
        }
    }
}

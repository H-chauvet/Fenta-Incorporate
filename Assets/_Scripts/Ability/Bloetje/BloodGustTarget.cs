using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BloodGustTarget : MonoBehaviour
{
    public UnityEvent onHit;
    
    public void OnHit()
    {
        onHit.Invoke();
    }
}
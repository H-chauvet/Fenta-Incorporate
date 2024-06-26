using UnityEngine;
using UnityEngine.Events;

public class ChestTrigger : MonoBehaviour
{
    public UnityEvent onGrab;
    public UnityEvent onAnimationStart;
    
    public void OnGrab()
    {
        onGrab.Invoke();
    }

    public void OnAnimationStart()
    {
        onAnimationStart.Invoke();
    }
}

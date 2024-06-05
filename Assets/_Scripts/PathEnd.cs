using UnityEngine;
using UnityEngine.Events;

public class PathEnd : MonoBehaviour
{
    [SerializeField] private UnityEvent onPathEnded;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPathEnded?.Invoke();
        }
    }
}

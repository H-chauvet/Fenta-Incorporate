using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private Transform respawnPosition;
    
    private void OnTriggerEnter(Collider other)
    {
        if (respawnPosition != null && other.CompareTag("Player"))
        {
            other.transform.position = respawnPosition.position;
        }
    }
}

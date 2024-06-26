using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    [SerializeField] private float destroyAfterSeconds;
    
    private void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }
}

using UnityEngine;

public class PlayerPath : MonoBehaviour
{
    [SerializeField]
    private void Awake()
    {
        var meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }
    }

    private void SetDollyLength()
    {
        
    }
}

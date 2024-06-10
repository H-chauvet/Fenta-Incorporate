using UnityEngine;

public class InteractionIndicator : MonoBehaviour
{
    [SerializeField] private Transform interactableObject;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.position = _camera.WorldToScreenPoint(interactableObject.position);
    }
}

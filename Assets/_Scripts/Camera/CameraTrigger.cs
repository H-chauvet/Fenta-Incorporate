using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Animator cameraStateAnimator;
    private static readonly int NextCamera = Animator.StringToHash("NextCamera");

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraStateAnimator.SetTrigger(NextCamera);
        }
    }
}

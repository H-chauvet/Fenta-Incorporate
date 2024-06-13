using Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")]
public class LockCameraAxis : CinemachineExtension
{
    [SerializeField] private bool lockX, lockY, lockZ;
    [SerializeField] private float xPosition, yPosition, zPosition;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase virtualCamera, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 position = state.RawPosition;
            
            if (lockX)
                position.x = xPosition;
            if (lockY)
                position.y = yPosition;
            if (lockZ)
                position.z = zPosition;

            state.RawPosition = position;
        }
    }
}

using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> cameras;

    private CinemachineVirtualCamera _currentCamera;

    private void Start()
    {
        foreach (var virtualCamera in cameras)
        {
            virtualCamera.enabled = false;
        }

        _currentCamera = cameras[0];
        cameras[0].enabled = true;
    }

    public void NextCamera()
    {
        var currentCameraIndex = cameras.IndexOf(_currentCamera);
        var nextCamera = cameras[currentCameraIndex + 1];
        _currentCamera.enabled = false;
        nextCamera.enabled = true;
        _currentCamera = nextCamera;
    }
    
    public void PreviousCamera()
    {
        var currentCameraIndex = cameras.IndexOf(_currentCamera);
        var previousCamera = cameras[currentCameraIndex - 1];
        _currentCamera.enabled = false;
        previousCamera.enabled = true;
        _currentCamera = previousCamera;
    }
}

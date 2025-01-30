using Unity.Cinemachine;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _rotatePlatformSpeed = 100f;
    [SerializeField] private float _cameraRotateSpeed = 400f;
    [SerializeField] private float _zoomSpeed = 10.0f;
    [SerializeField] private float _maxYAngel = 40f;
    [SerializeField] private float _minYAngel = 340f;
    [SerializeField] private float _maxZoom = 30f;
    [SerializeField] private float _mixZoom = 1.5f;
    [SerializeField] private Transform _rotatePlatform;
    [SerializeField] private Transform _cameraRotatorX;
    [SerializeField] private Transform _cameraRotatorY;
    [SerializeField] private CinemachineThirdPersonFollow _cameraSettings;

    void Update()
    {
        if ((-2 < Input.GetAxis("Mouse Y")) && (Input.GetAxis("Mouse Y") < 2))
        {
            GetCameraRotation();
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            GetZoom();
        
        if (!Input.GetMouseButton(2))
            GetCityPlatformRotate();
    }
    
    private void GetCameraRotation()
    {
            float dx = Input.GetAxis("Mouse X") * _cameraRotateSpeed * Time.deltaTime;
            float dy = Input.GetAxis("Mouse Y") * _cameraRotateSpeed * Time.deltaTime * -1;
            
            _cameraRotatorX.localRotation *= Quaternion.Euler(new Vector3(0, dx, 0));
            
            var rotateY = (_cameraRotatorY.localRotation * Quaternion.Euler(new Vector3(dy, 0, 0))).eulerAngles.x;
            
            if (((_minYAngel < rotateY) && (rotateY < 360)) || ((rotateY < _maxYAngel) && (rotateY > 0)))
                _cameraRotatorY.localRotation *= Quaternion.Euler(new Vector3(dy, 0, 0));
    }

    private void GetZoom()
    {
        float dz = Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed * Time.deltaTime * -1;
        
        if (((_mixZoom < (_cameraSettings.CameraDistance + dz*3)) && ((_cameraSettings.CameraDistance + dz*3) < _maxZoom)))
        {
            _cameraSettings.CameraDistance += dz*3;
            _cameraSettings.VerticalArmLength += dz;
        }
    }

    private void GetCityPlatformRotate()
    {
        _rotatePlatform.localRotation = Quaternion.RotateTowards(_rotatePlatform.localRotation, _cameraRotatorX.localRotation, _rotatePlatformSpeed * Time.deltaTime);
    }
}

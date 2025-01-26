using System;
using UnityEngine;

public class RotationCameraTarget : MonoBehaviour
{
    //[SerializeField] private float _zoomSpeed = 10.0f;
    [SerializeField] private float _rotateSpeed = 100f;
    [SerializeField] private float _freeRotateSpeed = 400f;
    [SerializeField] private float _maxYAngel = 30f;
    [SerializeField] private float _minYAngel = 350f; 
    [SerializeField] private Transform _rotatePlatform;
    [SerializeField] private Transform _freeCameraRotator;
    
    private Vector2 _rotateStartPoint;
    private Vector2 _rotateEndPoint;

    private void Start()
    {
        //ZeroCameraPosition();
    }

    void Update()
    {
        if (Input.GetMouseButton(2))
            GetCameraFreeRotation();
        else
            GetCameraRotation();
        
        //if (Input.GetMouseButtonUp(2))
            //_freeCameraRotator.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
    
    private void GetCameraRotation()
    {
            float dx = Input.GetAxis("Mouse X") * _freeRotateSpeed * Time.deltaTime;
            //float dx2 = Input.GetAxis("Mouse X") * _rotateSpeed * Time.deltaTime;
            float dy = Input.GetAxis("Mouse Y") * _freeRotateSpeed * Time.deltaTime * -1;
            
            _freeCameraRotator.localRotation *= Quaternion.Euler(new Vector3(0, dx, 0));
            _rotatePlatform.localRotation = Quaternion.Lerp(_rotatePlatform.localRotation, _freeCameraRotator.localRotation, _rotateSpeed * Time.deltaTime);
            //_rotatePlatform.localRotation *= Quaternion.Euler(new Vector3(0, dx2, 0));
            
            var rotateY = (transform.localRotation * Quaternion.Euler(new Vector3(dy, 0, 0))).eulerAngles.x;
            
            if (((_minYAngel < rotateY) && (rotateY < 360)) || ((rotateY < _maxYAngel) && (rotateY > 0)))
                transform.localRotation *= Quaternion.Euler(new Vector3(dy, 0, 0));
    }

    private void GetCameraFreeRotation()
    {
        float dx = Input.GetAxis("Mouse X") * _freeRotateSpeed * Time.deltaTime;
        float dy = Input.GetAxis("Mouse Y") * _freeRotateSpeed * Time.deltaTime * -1;
        
       _freeCameraRotator.localRotation *= Quaternion.Euler(new Vector3(0, dx, 0));
            
        var rotateY = (transform.localRotation * Quaternion.Euler(new Vector3(dy, 0, 0))).eulerAngles.x;
            
        if (((_minYAngel < rotateY) && (rotateY < 360)) || ((rotateY < _maxYAngel) && (rotateY > 0)))
            transform.localRotation *= Quaternion.Euler(new Vector3(dy, 0, 0));
    }

    private void ZeroCameraPosition()
    {
        _rotatePlatform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        _freeCameraRotator.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}

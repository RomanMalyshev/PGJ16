using UnityEngine;
using UnityEngine.UI;

public class crosshair : MonoBehaviour
{
    [SerializeField] private Image _crosshair;
    [SerializeField] private float _range;
    [SerializeField] private Camera _camera;
    
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        var targetpoint = ray.GetPoint(_range);
        _crosshair.rectTransform.position = new Vector3(_camera.WorldToScreenPoint(targetpoint).x, Screen.height/2, _camera.WorldToScreenPoint(targetpoint).z);
    }
}

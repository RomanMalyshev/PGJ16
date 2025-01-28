using UnityEngine;

public class WASDController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _cameraDirektion;
    
    void Update()
    {
        GetCityMoved();
    }

    private void GetCityMoved()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);
        
        if (moveDirection.magnitude >= 0.1f)
        {
            float rotationAngel = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + _cameraDirektion.eulerAngles.y;
            Vector3 move = Quaternion.Euler(0f, rotationAngel, 0f) * Vector3.forward;
            _controller.Move(move * _moveSpeed * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player movement script

    public float MoveSpeed  = 3.8f;
    public float RotSpeed  = 80.0f;
    [SerializeField] private Transform _cameraDirektion;
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);

        if (moveDirection.magnitude >= 0.1f)
        {
            float rotationAngel = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + _cameraDirektion.eulerAngles.y;
            Vector3 move = Quaternion.Euler(0f, rotationAngel, 0f) * Vector3.forward;
           // _controller.Move(move * MoveSpeed * Time.deltaTime);
            transform.Translate(move * (MoveSpeed * Time.deltaTime));
        }
        
    }
}

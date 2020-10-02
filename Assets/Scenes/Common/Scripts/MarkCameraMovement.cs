using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkCameraMovement : MonoBehaviour
{

    [SerializeField] float movementSensitivity = 100f;
    [SerializeField] Transform playerBody;
    [SerializeField] bool InverseYAxis = true;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float movementX = Input.GetAxis("Right Analog Stick (Horizontal)") * movementSensitivity * Time.deltaTime;
        float movementY = Input.GetAxis("Right Analog Stick (Vertical)") * movementSensitivity * Time.deltaTime;
        
        if ( InverseYAxis )
        {
            movementY *= -1;
        }

        xRotation -= movementY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * movementX);

    }
}

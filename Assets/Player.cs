using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float playerMovementSpeed = 1.5f;
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if ( Input.GetKey(KeyCode.UpArrow))
        //{
        //    rigidBody.MovePosition(Vector3.forward);
            
        //}
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rigidBody.velocity = new Vector3(moveHorizontal * playerMovementSpeed, rigidBody.velocity.y, moveVertical * playerMovementSpeed);
    }
}

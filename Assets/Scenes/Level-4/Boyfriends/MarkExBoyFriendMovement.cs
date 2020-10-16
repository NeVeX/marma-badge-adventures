//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MarkExBoyFriendMovement : MonoBehaviour
//{
//    [SerializeField] GameObject Player;
//    [SerializeField] float MoveSpeed = 2.0f;

//    //private Rigidbody _rigidBody;
//    //private void Start()
//    //{
//    //    _rigidBody = GetComponent<Rigidbody>();
//    //}

//    private void Update()
//    {
//        // Follow and move towards Player
//        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);
//    }
//}

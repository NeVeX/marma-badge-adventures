using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkExBoyFriend : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float MoveSpeed = 2.0f;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        OnSearchingForPlayer();
    }

    private void Update()
    {
        // Follow and move towards Player
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ExBoyfriend"))
        {
            Debug.Log("Ex-boyfriend hit player");
            OnHitPlayer();
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ExBoyfriend"))
        {
            Debug.Log("Ex-boyfriend left hitting player");
            OnSearchingForPlayer();
        }
    }

    public void OnPlayerColliderHit()
    {
        Debug.Log("Player Hit ex-boyfriend");
        OnHitPlayer();
    }

    private void OnHitPlayer()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        //_rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnSearchingForPlayer()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        //_rigidbody.velocity = Vector3.zero;
    }
}

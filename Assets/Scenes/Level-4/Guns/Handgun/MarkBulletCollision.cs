using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBulletCollision : MonoBehaviour
{
    [SerializeField] bool DestroyOnCollision = true;
    [SerializeField] bool CanHitPlayer = false;

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if ( DestroyOnCollision && (CanHitPlayer && collision.gameObject.CompareTag("Player"))) // shooting out from player, ignore those
        {
            Debug.Log("Destroying bullet on impact with " + collision.gameObject.tag);
            Destroy(gameObject, 0.8f);
        }
    }
}

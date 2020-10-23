using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.2f); // destory this after a pause
        } else
        {
            Destroy(gameObject, 1.0f); // destory this after a pause
        }

    }

}

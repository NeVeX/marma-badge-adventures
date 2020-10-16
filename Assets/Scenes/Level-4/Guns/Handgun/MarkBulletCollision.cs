using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBulletCollision : MonoBehaviour
{
    [SerializeField] bool DestroyOnCollision = true;

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if ( DestroyOnCollision)
        {
            //Debug.Log("Destroying bullet on impact");
            Destroy(gameObject, 0.8f);
        }
    }
}

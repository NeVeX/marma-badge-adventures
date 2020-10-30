using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkProjectile : MonoBehaviour
{
    [SerializeField] public bool CanPlayerGrab = true;
    private bool _canCollide = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (!_canCollide)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            OnPlayerColliderHit();
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 0.17f);
        }
    }

    public void OnPlayerColliderHit()
    {
        Destroy(gameObject);
    }

    public void TurnOffCollision()
    {
        _canCollide = false;
    }
    
    public void TurnOnCollision()
    {
        _canCollide = true;
    }
}

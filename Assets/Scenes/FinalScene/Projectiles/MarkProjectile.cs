using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkProjectile : MonoBehaviour
{
    [SerializeField] public bool CanPlayerGrab = true;
    
    public bool ShouldRotate = false;
    [SerializeField] float SpeedRotation = 200.0f;
    [SerializeField] public AudioSource PlayerGrabAudioSource;

    private bool _canCollide = true;

    public void Update()
    {
        if (ShouldRotate)
        {
            transform.Rotate(Vector3.up, SpeedRotation * Time.deltaTime);
            transform.Rotate(Vector3.right, (SpeedRotation / 4) * Time.deltaTime);
        }
    }

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
        else if (collision.gameObject.name.Equals("Boss"))
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

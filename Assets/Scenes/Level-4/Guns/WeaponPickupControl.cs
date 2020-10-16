using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WeaponPickupControl : MonoBehaviour
{
    [SerializeField] float Speed = 10.0f;
    [SerializeField] MarkGunManager MarkGunManager;
    [SerializeField] SpawnManager SpawnManager;

    private void Start()
    {
        Assert.IsNotNull(MarkGunManager);
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Weapon pickup collided with " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerHit();
        }
    }

    private void OnPlayerHit()
    {
        MarkGunManager.ActivateGun(gameObject.tag);
        SpawnManager.ActivateSpawners();
        Destroy(gameObject);
    }
}

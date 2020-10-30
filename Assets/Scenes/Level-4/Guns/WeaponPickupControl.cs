using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WeaponPickupControl : MonoBehaviour
{
    [SerializeField] float Speed = 10.0f;
    [SerializeField] GameObject WeaponGameobject;
    [SerializeField] MarkGunManager MarkGunManager;
    [SerializeField] SpawnManager SpawnManager;
    [SerializeField] AudioSource AudioToPlayOnPickup;

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
        MarkGunManager.ActivateGun(WeaponGameobject.name);
        SpawnManager.ActivateSpawners();
        if (AudioToPlayOnPickup != null)
        {
            AudioToPlayOnPickup.Play();
        }
        Destroy(gameObject);
    }
}

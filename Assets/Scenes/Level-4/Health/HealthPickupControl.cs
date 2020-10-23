using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupControl : MonoBehaviour
{
    [SerializeField] float HealthAmount = 100.0f;
    [SerializeField] float Speed = 10.0f;
    [SerializeField] MarkPlayerCollision MarkPlayerCollision;
    [SerializeField] SpawnManager SpawnManager;

    private void Start()
    {
        UnityEngine.Assertions.Assert.IsNotNull(MarkPlayerCollision);
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Health pickup collided with " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerPickup();
        }
    }

    private void OnPlayerPickup()
    {
        MarkPlayerCollision.AddHealth(HealthAmount);
        if (SpawnManager != null)
        {
            SpawnManager.ActivateSpawners();
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBoyfriendShooter : MonoBehaviour
{

    [SerializeField] private GameObject ShootObject;
    [SerializeField] private Transform ShootObjectLocation;

    [SerializeField] private float destroyTimeSeconds = 5f;
    [SerializeField] private float shotPower = 500f;
    [SerializeField] private float ejectPower = 150f;

    [SerializeField] AudioSource ShootAudio;
    [SerializeField] float ShotEverySecondsMin = 3.0f;
    [SerializeField] float ShotEverySecondsMax = 6.0f;

    private float _nextTimeAllowedToFire = 0.0f;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
        SetNextTimeCanFire();
    }

    private void SetNextTimeCanFire()
    {
        _nextTimeAllowedToFire = Time.unscaledTime + UnityEngine.Random.Range(ShotEverySecondsMin, ShotEverySecondsMax);
    }

    void Update()
    {
        bool isAllowedToFire = Time.unscaledTime > _nextTimeAllowedToFire;

        if (isAllowedToFire)
        {
            Shoot();
            SetNextTimeCanFire();
        }
    }

    //This function creates the bullet behavior
    void Shoot()
    {
        // Get the crosshair ray
        // Create a bullet and add force on it in direction of the barrel in the direction of the crosshair
        Transform aimLookat = _player.transform;
        ShootObjectLocation.transform.LookAt(aimLookat);
        GameObject shootObject = Instantiate(ShootObject, ShootObjectLocation.position, ShootObjectLocation.rotation);
        //ShootObjectLocation.transform.LookAt(aimLookat); // look at the target (crosshair) and shoot towards that direction
        Rigidbody shootObjectRB = shootObject.GetComponent<Rigidbody>();
        //shootObject.transform.LookAt(aimLookat);
        //shootObjectRB.transform.LookAt(aimLookat);
        shootObjectRB.AddForce(ShootObjectLocation.forward * shotPower);
        Destroy(shootObject, destroyTimeSeconds); // destroy after a few seconds

        if (ShootAudio != null)
        {
            ShootAudio.Play();
        }

    }

}

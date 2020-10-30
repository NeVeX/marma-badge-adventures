using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MarkBoss : MonoBehaviour
{
    [SerializeField] HealthBarControl HealthBarControl;
    [SerializeField] float Health = 100f;
    [SerializeField] GameObject[] Projectiles;
    [SerializeField] private Transform ShootObjectLocation;

    [SerializeField] private float destroyTimeSeconds = 5f;
    [SerializeField] private float shotPower = 500f;
    [SerializeField] private float ejectPower = 150f;

    [SerializeField] float ShotEverySecondsMin = 1.0f;
    [SerializeField] float ShotEverySecondsMax = 1.1f;

    private float _nextTimeAllowedToFire = 0.0f;
    private GameObject _player;

    void Start()
    {
        HealthBarControl.SetMaxHealth(Health);
        _player = GameObject.Find("Player");
        Assert.IsNotNull(_player);
        SetNextTimeCanFire(6.0f); // small buffer
    }

    private void LateUpdate()
    {
        HealthBarControl.SetHealth(Health);
    }

    private void SetNextTimeCanFire(float extraSeconds = 0.0f)
    {
        _nextTimeAllowedToFire = Time.unscaledTime + UnityEngine.Random.Range(ShotEverySecondsMin, ShotEverySecondsMax) + extraSeconds;
    }

    // Update is called once per frame
    void Update()
    {

        bool isAllowedToFire = Time.unscaledTime > _nextTimeAllowedToFire;
        if (isAllowedToFire)
        {
            LaunchProjectile();
            SetNextTimeCanFire();
        }

        // Look at the player all the time
        Vector3 lookAtPosition = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        transform.LookAt(lookAtPosition);
    }

    void LaunchProjectile()
    {
        GameObject projectileToLaunch = PickProjectileToFire();


        Transform aimLookat = _player.transform;
        ShootObjectLocation.transform.LookAt(aimLookat);
        GameObject shootObject = Instantiate(projectileToLaunch, ShootObjectLocation.position, ShootObjectLocation.rotation);
        Rigidbody shootObjectRB = shootObject.GetComponent<Rigidbody>();
        shootObjectRB.AddForce(ShootObjectLocation.forward * shotPower);
        Destroy(shootObject, destroyTimeSeconds); // destroy after a few seconds
    }

    private GameObject PickProjectileToFire()
    {
        return Projectiles[Random.Range(0, Projectiles.Length)];
    }

    private void OnCollisionEnter(Collision collision)
    {
        MarkDamageObject mdo = collision.gameObject.GetComponent<MarkDamageObject>();

        if (mdo != null && !mdo.IsEnemy)
        {
            OnTakeDamage(mdo);
        }
       
        if (collision.gameObject.CompareTag("Player"))
        {
            OnHitPlayer();
        }

    }

    private void OnTakeDamage(MarkDamageObject damageObject)
    {
        Health -= damageObject.DamageAmount;
        Debug.Log("Boss taking damage of " + damageObject.DamageAmount + ", health left: " + Health);
        if (Health <= 0 || damageObject.IsInstantKill)
        {
            OnDestroyBoss();
        }
        else
        {
            //if (RandomAudioHurtPlayer != null)
            //{
            //    RandomAudioHurtPlayer.PlayRandomIfAllowed();
            //}
        }
    }

    public void OnPlayerColliderHit()
    {
        //OnHitPlayer();
    }

    private void OnHitPlayer()
    {
        // what to do?
    }

    private void OnDestroyBoss()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class MarkBoss : MonoBehaviour
{
    [SerializeField] AudioSource BossActivatedSound;
    [SerializeField] MarkProjectileWaveConfig[] WaveConfigs;
    [SerializeField] HealthBarControl HealthBarControl;
    [SerializeField] float Health = 100f;
    [SerializeField] private Transform[] ShootObjectLocations;
    [SerializeField] private float destroyTimeSeconds = 5f;
    [SerializeField] MarkRandomAudioPlayer ShootRandomAudioPlayer;
    [SerializeField] MarkRandomAudioPlayer HurtRandomAudioPlayer;

    private float _nextTimeAllowedToFire = 0.0f;
    private GameObject _player;
    private int _currentWaveConfigIndex;
    private bool _hasPlayedActivatedSound = false;
    private bool _hasInvokedPlayActivatedSound = false;

    void Start()
    {
        _currentWaveConfigIndex = WaveConfigs.Length - 1; // set this to zero on release
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
        _nextTimeAllowedToFire = Time.unscaledTime + UnityEngine.Random.Range(WaveConfigs[_currentWaveConfigIndex].ShotEverySecondsMin, WaveConfigs[_currentWaveConfigIndex].ShotEverySecondsMax) + extraSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        bool isAllowedToFire = Time.unscaledTime > _nextTimeAllowedToFire;
        if (Time.timeScale > 0f && isAllowedToFire && _hasPlayedActivatedSound)
        {
            LaunchProjectile();
            SetNextTimeCanFire();
        }

        if (!_hasInvokedPlayActivatedSound)
        {
            _hasInvokedPlayActivatedSound = true;
            StartCoroutine(PlayBossAwakeSound());
        }

        // Look at the player all the time
        Vector3 lookAtPosition = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        transform.LookAt(lookAtPosition);
    }

    private IEnumerator PlayBossAwakeSound()
    {
        _hasPlayedActivatedSound = false;
        BossActivatedSound.clip = WaveConfigs[_currentWaveConfigIndex].AudioToPlayOnActivated;
        float audioTime = BossActivatedSound.clip.length;
        BossActivatedSound.Play();
        yield return new WaitForSecondsRealtime(audioTime);
        _hasPlayedActivatedSound = true;
    }

    void LaunchProjectile()
    {
        GameObject projectileToLaunch = PickProjectileToFire();
        FixRotationsIfNeeded(projectileToLaunch);
        Transform aimLookat = _player.transform;
        Transform shootObjectLocation = ShootObjectLocations[Random.Range(0, ShootObjectLocations.Length)];
        shootObjectLocation.transform.LookAt(aimLookat);
        GameObject shootObject = Instantiate(projectileToLaunch, shootObjectLocation.position, shootObjectLocation.rotation);
        Rigidbody shootObjectRB = shootObject.GetComponent<Rigidbody>();
        shootObjectRB.AddForce(shootObjectLocation.forward * WaveConfigs[_currentWaveConfigIndex].GetShotPower());

        MarkProjectile mp = shootObject.GetComponent<MarkProjectile>();
        mp.ShouldRotate = true;

        if (ShootRandomAudioPlayer != null )
        {
            ShootRandomAudioPlayer.PlayRandomIfAllowed();
        }
        Destroy(shootObject, destroyTimeSeconds); // destroy after a few seconds
    }

    private void FixRotationsIfNeeded(GameObject gameObject)
    {
        MarkModelRotation[] rotationsFound = gameObject.GetComponentsInChildren<MarkModelRotation>();
         foreach (MarkModelRotation mr in rotationsFound)
        {
            mr.transform.Rotate(mr.FacingPlayerXDelta, mr.FacingPlayerYDelta, mr.FacingPlayerZDelta);
            //mr.transform.rotation.Set(mr.transform.rotation.x + mr.FacingPlayerXDelta, mr.transform.rotation.y + mr.FacingPlayerYDelta, mr.transform.rotation.z + mr.FacingPlayerZDelta, mr.transform.rotation.w);
            //mr.transform.rotation = new Quaternion(mr.transform.rotation.x + mr.FacingPlayerXDelta, mr.transform.rotation.y + mr.FacingPlayerYDelta, mr.transform.rotation.z + mr.FacingPlayerZDelta, mr.transform.rotation.w);
        }
    }

    private GameObject PickProjectileToFire()
    {
        return WaveConfigs[_currentWaveConfigIndex].GetNextProjectile();
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
        Debug.Log("Boss taking damage of " + damageObject.DamageAmount + " from "+ damageObject.name +". Health left: " + Health);
        if (Health <= 0 || damageObject.IsInstantKill)
        {
            OnDestroyBoss();
        }
        else
        {
            if (Health <= WaveConfigs[_currentWaveConfigIndex].LowHealthForNextStage)
            {
                // Move to the next stage (if we can)
                int nextWaveIndex = _currentWaveConfigIndex + 1;
                if (nextWaveIndex < WaveConfigs.Length)
                {
                    _currentWaveConfigIndex = nextWaveIndex;
                    _hasInvokedPlayActivatedSound = false;
                    Debug.Log("Moving to next stage of boss wave configurations");
                }
            }

            if (HurtRandomAudioPlayer != null)
            {
                HurtRandomAudioPlayer.PlayRandomNow();
            }
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




    //    void LaunchProjectileAngle()
    //    {



    //        GameObject projectileToLaunch = PickProjectileToFire();
    //        Transform aimLookat = _player.transform;
    //        ShootObjectLocation.transform.LookAt(aimLookat);
    //        GameObject shootObject = Instantiate(projectileToLaunch, ShootObjectLocation.position, ShootObjectLocation.rotation);

    //        float firingAngle = 45.0f;
    //        float gravity = 9.8f;


    //        // Move projectile to the position of throwing object + add some offset if needed.
    //        //Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

    //        // Calculate distance to target
    //        float target_Distance = Vector3.Distance(shootObject.transform.position, aimLookat.position);

    //        // Calculate the velocity needed to throw the object to the target at specified angle.
    //        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

    //        // Extract the X  Y componenent of the velocity
    //        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
    //        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

    //        // Calculate flight time.
    //        float flightDuration = target_Distance / Vx;

    //        // Rotate projectile to face the target.
    //        //Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);


    //        //Vector3 vel = //new Vector3(Vx * direction.x, Vy);
    //        ShootObjectLocation.forward.Set(ShootObjectLocation.forward.x * Vx, ShootObjectLocation.forward.y * Vy, ShootObjectLocation.forward.z);
    //        //GameObject go = Instantiate(granade, transform.position + posOffest, new Quaternion());

    //        Rigidbody shootObjectRB = shootObject.GetComponent<Rigidbody>();
    //        shootObjectRB.AddForce(ShootObjectLocation.forward * WaveConfigs[_currentWaveConfigIndex].GetShotPower());

    //        //shootObjectRB.AddForce(vel );
    //        //go.GetComponent<Rigidbody2D>().AddTorque(torque);


    ////        float elapse_time = 0;


    //        //while (elapse_time < flightDuration)
    //        //{
    //        //    //shootObject.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
    //        //    Rigidbody shootObjectRB = shootObject.GetComponent<Rigidbody>();
    //        //    shootObjectRB.AddForce(ShootObjectLocation.forward * WaveConfigs[_currentWaveConfigIndex].GetShotPower());
    //        //    elapse_time += Time.deltaTime;

    //        //    yield return null;
    //        //}
    //    }


}

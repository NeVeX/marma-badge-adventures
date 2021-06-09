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
    [SerializeField] MarkExplosion MarkExplosion;
    [SerializeField] AudioClip BossDeathSound;
    [SerializeField] GameObject RabbitModel;
    [SerializeField] ScreenMessageControl EndingScreenControl;

    private MarkProjectileManager _markPlayerProjectileManager;
    private float _nextTimeAllowedToFire = 0.0f;
    private GameObject _player;
    private int _currentWaveConfigIndex;
    private bool _hasPlayedActivatedSound = false;
    private bool _hasInvokedPlayActivatedSound = false;
    private bool _isBossDestroyed = false;

    void Start()
    {
        _currentWaveConfigIndex = 0; // WaveConfigs.Length - 1; // set this to zero on release
        HealthBarControl.SetMaxHealth(Health);
        _player = GameObject.Find("Player");
        Assert.IsNotNull(_player);

        _markPlayerProjectileManager = GameObject.FindObjectOfType<MarkProjectileManager>();
        Assert.IsNotNull(_markPlayerProjectileManager);

        //SetNextTimeCanFire(5.0f); // small buffer
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
        if (_isBossDestroyed )
        {
            return;
        }
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
        float audioTime = BossActivatedSound.clip.length + 1.0f; // slight buffer
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
        MarkProjectile mp = _markPlayerProjectileManager.GetCurrentlyHeldProjectile();
        //Debug.Log("Player is holding " + mp?.name);
        return WaveConfigs[_currentWaveConfigIndex].GetNextProjectile(mp);
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
        if ( damageObject.DamageAmount <= 0)
        {
            return;
        }

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
        _isBossDestroyed = true;
        float explosionTime = MarkExplosion.Explode();
        float deathSoundTime = BossDeathSound.length;
        BossActivatedSound.clip = BossDeathSound;
        BossActivatedSound.Play();

        float waitTime = Mathf.Max(explosionTime, deathSoundTime);

        //Destroy(RabbitModel); // destroy the boss
        RabbitModel.SetActive(false);

        // wait a while before next set of actions
        StartCoroutine(ShowEndingMessage(waitTime));
    }
    
    private IEnumerator ShowEndingMessage(float timeUntilShow)
    {
        yield return new WaitForSeconds(timeUntilShow);
        Debug.Log("Showing Ending Screen Control after boss defeated");
        EndingScreenControl.ShowMessage();


        // Fix: remove the "boss" still from the scene in the way of the ending objects
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.detectCollisions = false;
        }
        // hide it too
        transform.position = new Vector3(5000, 5000, 5000);
    }

}

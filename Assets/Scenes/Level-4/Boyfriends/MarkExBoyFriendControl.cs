using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkExBoyFriendControl : MonoBehaviour
{
    [SerializeField] HealthBarControl HealthBarControl;
    [SerializeField] MarkRandomAudioPlayer RandomAudioHurtPlayer;
    [SerializeField] MarkRandomAudioPlayer RandomAudioDeadPlayer;
    [SerializeField] MarkRandomAudioPlayer RandomAudioYellPlayer;
    [SerializeField] MarkExplosion MarkExplosion;
    [SerializeField] bool IsStationary = false;
    //[SerializeField] GameObject Player;
    [SerializeField] float MinMoveSpeed = 1.0f;
    [SerializeField] float MaxMoveSpeed = 3.0f;
    [SerializeField] float Health = 100f;

    private GameObject _player;
    private MarkPlayerCollision _playerCollisionControl;
    private Rigidbody _rigidbody;
    private GameObject _spawnObject;
    private int _spawnerID;
    private float _moveSpeed;
    private bool _shouldMove = true;
    private bool _isBeenDestroyed = false;

    // this gets called in the beginning when it is created by the spawner script
    void setName(int spawnerID)
    {
        _spawnerID = spawnerID;
    }

    private void Start()
    {
        HealthBarControl.SetMaxHealth(Health);
        _player = GameObject.FindWithTag("Player");
        _playerCollisionControl = _player.GetComponent<MarkPlayerCollision>();

        _rigidbody = GetComponent<Rigidbody>();
        _spawnObject = GameObject.FindWithTag("Spawner");
        _moveSpeed = Random.Range(MinMoveSpeed, MaxMoveSpeed);
        OnSearchingForPlayer();
    }

    private void FixedUpdate()
    {
        if (_shouldMove && !IsStationary)
        {
            // Follow and move towards Player
            //transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);
            Vector3 newPosition = Vector3.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
            _rigidbody.MovePosition(newPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collided with gameobject tag: " + collision.gameObject.name);
        MarkDamageObject mdo = collision.gameObject.GetComponent<MarkDamageObject>();
        
        if (mdo != null && !mdo.IsEnemy)
        {
            OnTakeDamage(mdo);
        }
        //else
        //{
        //    Debug.Log("Ex-boyfriend hit something that did not have a MarkDamageObject attached");
        //}

        //if ( collision.gameObject.CompareTag("Untagged")) { return; }

        // Debug.Log("Ex-boyfriend ENTER hit something. Tag = " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            OnHitPlayer();
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Untagged")) { return; }

        // Debug.Log("Ex-boyfriend EXIT hit something. Tag = " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            OnSearchingForPlayer();
        }
    }

    public void OnPlayerColliderHit()
    {
        //Debug.Log("Player Hit ex-boyfriend");
        //OnHitPlayer();
    }

    private void LateUpdate()
    {
        HealthBarControl.SetHealth(Health);
    }

    private void OnTakeDamage(MarkDamageObject damageObject)
    {
        Health -= damageObject.DamageAmount;
        Debug.Log("Ex-Boyfriend taking damage of " + damageObject.DamageAmount + ", health left: " + Health);
        if ( Health <= 0 || damageObject.IsInstantKill)
        {
            OnDestroyBoyfriend();
        } else
        {
            if (RandomAudioHurtPlayer != null)
            {
                RandomAudioHurtPlayer.PlayRandomIfAllowed();
            }
        }
    }

    public void OnDestroyBoyfriend()
    {
        if (_isBeenDestroyed)
        {
            return;
        }
        _isBeenDestroyed = true;
        Debug.Log("Destroying ex-boyfriend");
        _spawnObject.BroadcastMessage("killEnemy", _spawnerID);


        if (RandomAudioYellPlayer != null)
        {
            RandomAudioYellPlayer.StopPlaying(); // stop all audio yelling
        }
        if (RandomAudioHurtPlayer != null)
        {
            RandomAudioHurtPlayer.StopPlaying();
        }

        _shouldMove = false; // don't move

        _playerCollisionControl.OnKilledEnemy();

        float explosionTime = MarkExplosion.Explode();
        deactivateAllChildren(); // hide all children (models etc) until we can destroy this object

        // Play dead sound 
        float audioTime = (RandomAudioDeadPlayer != null) ? RandomAudioDeadPlayer.PlayRandomNow() : 0f;
        Destroy(gameObject, Mathf.Max(explosionTime, audioTime)); // destroy after the above "things" play
    }    

    private void deactivateAllChildren()
    {
        // Destroy everything except this script
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.GetComponent<MarkExplosion>() != null )
            {
                continue; // skip this
            }
            child.SetActive(false);
            //Destroy(gameObject.transform.GetChild(i));
        }
    }

    private void OnHitPlayer()
    {
        OnDestroyBoyfriend();
        //_shouldMove = false;
        //Debug.Log("ExBoyfriend cannot move");
        //_rigidbody.isKinematic = true;
        //_rigidbody.useGravity = true;
        ////_rigidbody.velocity = Vector3.zero;
        //_rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnSearchingForPlayer()
    {
        _shouldMove = true;
        //Debug.Log("ExBoyfriend can move");
        //_rigidbody.isKinematic = false;
        //_rigidbody.useGravity = true;
        //_rigidbody.velocity = Vector3.zero;
    }
}

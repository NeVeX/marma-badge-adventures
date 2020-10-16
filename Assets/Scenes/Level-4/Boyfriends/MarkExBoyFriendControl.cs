using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkExBoyFriendControl : MonoBehaviour
{
    [SerializeField] MarkRandomAudioPlayer RandomAudioHurtPlayer;
    [SerializeField] MarkRandomAudioPlayer RandomAudioDeadPlayer;
    [SerializeField] MarkRandomAudioPlayer RandomAudioYellPlayer;

    //[SerializeField] GameObject Player;
    [SerializeField] float MoveSpeed = 2.0f;
    [SerializeField] float Health = 100f;

    private GameObject _player;
    private Rigidbody _rigidbody;
    private GameObject _spawnObject;
    private int _spawnerID;
    private bool _shouldMove = true;

    // this gets called in the beginning when it is created by the spawner script
    void setName(int spawnerID)
    {
        _spawnerID = spawnerID;
    }

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _rigidbody = GetComponent<Rigidbody>();
        _spawnObject = GameObject.FindWithTag("Spawner");
        OnSearchingForPlayer();
    }

    private void FixedUpdate()
    {
        if (_shouldMove)
        {
            // Follow and move towards Player
            //transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);
            Vector3 newPosition = Vector3.MoveTowards(transform.position, _player.transform.position, MoveSpeed * Time.deltaTime);
            _rigidbody.MovePosition(newPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        MarkDamageObject mdo = collision.gameObject.GetComponent<MarkDamageObject>();
        if (mdo != null)
        {
            OnTakeDamage(mdo.GetDamageAmount());
        }
        else
        {
            Debug.Log("Ex-boyfriend hit something that did not have a MarkDamageObject attached");
        }

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

    private void OnTakeDamage(float damageToTake)
    {
        Health -= damageToTake;
        Debug.Log("Ex-Boyfriend taking damage of " + damageToTake + ", health left: " + Health);
        if ( Health <= 0)
        {
            OnDestroyBoyfriend();
        } else
        {
            RandomAudioHurtPlayer.PlayRandom();
        }
    }

    private void OnDestroyBoyfriend()
    {
        Debug.Log("Destroying ex-boyfriend");
        _spawnObject.BroadcastMessage("killEnemy", _spawnerID);

        RandomAudioYellPlayer.StopPlaying(); // stop all audio yelling
        RandomAudioHurtPlayer.StopPlaying();

        _shouldMove = false; // don't move

        deactivateAllChildren(); // hide all children (models etc) until we can destroy this object.

        // Play dead sound 
        float playTime = RandomAudioDeadPlayer.PlayRandom();
        Destroy(gameObject, playTime); // destroy after the sound plays
    }    

    private void deactivateAllChildren()
    {
        // Destroy everything except this script
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
            //Destroy(gameObject.transform.GetChild(i));
        }
    }

    private void OnHitPlayer()
    {
        _shouldMove = false;
        Debug.Log("ExBoyfriend cannot move");
        //_rigidbody.isKinematic = true;
        //_rigidbody.useGravity = true;
        ////_rigidbody.velocity = Vector3.zero;
        //_rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnSearchingForPlayer()
    {
        _shouldMove = true;
        Debug.Log("ExBoyfriend can move");
        //_rigidbody.isKinematic = false;
        //_rigidbody.useGravity = true;
        //_rigidbody.velocity = Vector3.zero;
    }

    private void PlayHurtAudio()
    {

    }

    private void PlayDeathAudio()
    {

    }
}

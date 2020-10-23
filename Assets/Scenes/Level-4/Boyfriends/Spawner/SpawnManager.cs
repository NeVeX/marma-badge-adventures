using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] SpawnManager NextSpawnManager;
    [SerializeField] GameObject[] ObjectsToActivate;
    [SerializeField] Spawner OverideEndingSpawner;

    private Spawner[] _spawners;
    private int _spawnersLength;
    private int _spawnersCompleted = 0;
    private bool _alreadyActivated = false;
    private bool _alreadyCompleted = false;

    void Start()
    {
        _spawners = GetComponentsInChildren<Spawner>();
        _spawnersLength = _spawners.Length;
        Assert.IsTrue(_spawners != null && _spawners.Length > 0);
        Debug.Log(gameObject.name + ": Amount of spawners is in this wave is " + _spawners.Length);
    }

    private void Update()
    {
        if (!_alreadyCompleted && OverideEndingSpawner != null && OverideEndingSpawner.IsCompleted())
        {
            // Destroy all the spawners
            Debug.Log("Overiding spawner is completed");
            _spawners.ToList().ForEach(sp => overrideDestroy(sp));
            OnAllSpawnersCompleted();
        }
    }

    private void overrideDestroy(Spawner spawner)
    {
        spawner.DisableSpawner();
        GameObject gameObj = spawner.gameObject;
        MarkExBoyFriendControl control = gameObj.GetComponent<MarkExBoyFriendControl>();
        if ( control == null )
        {
            control = gameObj.GetComponentInChildren<MarkExBoyFriendControl>();
        }

        if ( control != null )
        {
            control.OnDestroyBoyfriend();
        } else
        {
            Destroy(gameObj);
        }
    }

    public void ActivateSpawners()
    {
        if ( _alreadyActivated )
        {
            return;
        }
        Debug.Log(gameObject.name + ": Activating all spawners");
        _alreadyActivated = true;
        _spawners.ToList().ForEach(sp => sp.enableTrigger(OnSpawnerCompleted));
        if(ObjectsToActivate != null)
        {
            ObjectsToActivate.ToList().ForEach(obj => obj.SetActive(true));
        }
    }

    private void OnSpawnerCompleted()
    {
        _spawnersCompleted++;
        if ( _spawnersCompleted >= _spawnersLength)
        {
            OnAllSpawnersCompleted();
        }
    }

    private void OnAllSpawnersCompleted()
    {
        _alreadyCompleted = true;
        Debug.Log(gameObject.name + ": Spawner manager is done");
        if ( NextSpawnManager != null )
        {
            Debug.Log("Activating next spawn manager"); 
            NextSpawnManager.ActivateSpawners();
        }
    }

}
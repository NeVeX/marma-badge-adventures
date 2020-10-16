using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnManager : MonoBehaviour
{
    private Spawner[] _spawners;
    private int _spawnersLength;
    private int _spawnersCompleted = 0;
    private bool _alreadyActivated = false;

    void Start()
    {
        _spawners = GetComponentsInChildren<Spawner>();
        _spawnersLength = _spawners.Length;
        Assert.IsTrue(_spawners != null && _spawners.Length > 0);
        Debug.Log(gameObject.name + ": Amount of spawners is in this wave is " + _spawners.Length);
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
    }

    private void OnSpawnerCompleted()
    {
        _spawnersCompleted++;
        if ( _spawnersCompleted >= _spawnersLength)
        {
            Debug.Log(gameObject.name + ": Spawner manager is done");
        }
    }

}
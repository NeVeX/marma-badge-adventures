using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MarkExBoyFriendLookAt : MonoBehaviour
{
    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("Player");
        Assert.IsNotNull(_player);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAtPosition = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        transform.LookAt(lookAtPosition);
    }
}

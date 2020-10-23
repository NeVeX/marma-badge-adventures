using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkTesting : MonoBehaviour
{
    GameObject _player;

    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_player.transform);
    }
}

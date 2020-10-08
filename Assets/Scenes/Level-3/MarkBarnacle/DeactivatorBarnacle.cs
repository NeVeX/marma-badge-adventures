using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatorBarnacle : MonoBehaviour
{
    private CorridorEnemy _corridorEnemy;

    void Start()
    {
        _corridorEnemy = gameObject.transform.parent.GetComponentInChildren<CorridorEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBarnacle : MonoBehaviour
{

    private CorridorEnemy _corridorEnemy;

    void Start()
    {
        _corridorEnemy = gameObject.transform.parent.parent.GetComponentInChildren<CorridorEnemy>(); // Another hacky hack hackness
    }

    void OnPlayerColliderHit()
    {
        // This gets call alot, so buffer the "attack" or "damage"
        // Play duke nukem or doom cry

        //Debug.Log("Player hit the barnacle");
        if (_corridorEnemy != null )
        {
            _corridorEnemy.ActivateEnemy();
        }
    }
}

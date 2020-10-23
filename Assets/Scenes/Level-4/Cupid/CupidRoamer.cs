using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupidRoamer : MonoBehaviour
{
    [SerializeField] float RoamRadius = 5.0f;
    [SerializeField] float MoveSpeed = 1.5f;
    [SerializeField] float MinHeight = 5.5f;
    private Vector3 startPosition;
    private Vector3 moveToPosition;
    private float _timeToMoveToPosition = 0.0f;

    void Start()
    {
        startPosition = transform.position;
        moveToPosition = startPosition;
    }

    private void Update()
    {
        if ( transform.position == moveToPosition || _timeToMoveToPosition >= 9.0f) 
        {
            SetNewPositionToMoveTo();
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPosition, Time.deltaTime * MoveSpeed);
            _timeToMoveToPosition += Time.deltaTime;
        }
    }

    private void SetNewPositionToMoveTo()
    {
        moveToPosition = startPosition + (Random.insideUnitSphere * RoamRadius);
        moveToPosition.y = Mathf.Max(moveToPosition.y, MinHeight);
        _timeToMoveToPosition = 0.0f;
    }
}

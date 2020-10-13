using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorEnemy : MonoBehaviour
{
    [SerializeField] AudioSource SoundOnEnemyActivated;
    [SerializeField] GameObject DeactivatorsRemainingMessage;
    [SerializeField] GameObject NoDeactivatorsRemainingMessage;
    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject PositionEnd;
    [SerializeField] float Speed;
    [SerializeField] int HowManyActivationsAllowed = int.MaxValue;
    
    private int _numberOfDeActivators;
    private Vector3 _startingPosition;
    private bool _isActivated = false;

    void Start()
    {
        _startingPosition = Enemy.transform.position;
        _numberOfDeActivators = transform.parent.GetComponentsInChildren<DeactivatorBarnacle>().Length;
        Debug.Log("The number of deactivators in this area is " + _numberOfDeActivators);
        Reset();
    }

    void Update()
    {
        if ( _isActivated )
        {
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, PositionEnd.transform.position, Speed * Time.deltaTime);
            if (Vector3.Distance(Enemy.transform.position, PositionEnd.transform.position) < 0.001f)
            {
                Reset(); // reached the position end
                Debug.Log("Corridor Enemy reached position end");
            }
        }
    }

    public void ActivateEnemy()
    {
        if ( _isActivated ) { return; }

        HowManyActivationsAllowed--;
        if (HowManyActivationsAllowed >= 0)
        {
            _isActivated = true;
            SoundOnEnemyActivated.Play();
            Debug.Log("Corridor Enemy activated");
        } else
        {
            Debug.Log("Corridor Enemy not activated, no more activations left");
        }
    }

    public void DeactivateEnemy()
    {
        _numberOfDeActivators--;
        // show panel
        if (_numberOfDeActivators <= 0)
        {
            NoDeactivatorsRemainingMessage.GetComponent<ScreenMessageControl>().ShowMessage();
            Debug.Log("All deactivators reached");
            _isActivated = false;
            Enemy.SetActive(false); // deactivate it...should destroy instead?
            PositionEnd.SetActive(false);
        } else
        {
            DeactivatorsRemainingMessage.GetComponent<ScreenMessageControl>().ShowMessage();
        }
    }

    public void OnPlayerColliderHit()
    {
        // play some sound? Or do nothing?
    }

    private void Reset()
    {
        Enemy.transform.position = _startingPosition;
        _isActivated = false;
        if (HowManyActivationsAllowed <= 0)
        {
            DeactivateEnemy();
        }
    }
}

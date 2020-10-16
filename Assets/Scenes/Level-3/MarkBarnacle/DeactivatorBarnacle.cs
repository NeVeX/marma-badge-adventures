using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DeactivatorBarnacle : MonoBehaviour
{
    [SerializeField] float PlayerWithinRadius = 3.0f;
    [SerializeField] float PlayerLookAtOffsetTolerance = 0.85f;
    [SerializeField] float FadingDurationSeconds = 2.8f;
    private MeshRenderer _renderer;
    private Color _originalColor;
    private Color _fadedColor;
    private CorridorEnemy _corridorEnemy;
    private GameObject _player;
    private bool _alreadyUsed = false;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _originalColor = _renderer.material.color;
        _fadedColor = new Color(_originalColor.r, _originalColor.g, _originalColor.b, 0.0f); // transparent
        _corridorEnemy = gameObject.transform.parent.parent.GetComponentInChildren<CorridorEnemy>(); // COUGH...HACK...COUGH
        _player = GameObject.Find("Player");
        Assert.IsNotNull(_player);
    }

    // Update is called once per frame
    void Update()
    {
        if ( !_alreadyUsed && _renderer.isVisible)
        {
            if ( Input.GetButtonDown("A Button") && IsPlayerLookingAtThis() && IsPlayerWithinDistance())
            {
                Debug.Log("Deactivator barnacle interacted with player");
                OnDeactivation();
            }
        }
    }
    
    private void OnObjectedFaded()
    {
        Destroy(gameObject.transform.parent.gameObject); // hack
        Destroy(gameObject);
    }

    private void OnDeactivation()
    {
        _alreadyUsed = true;
        _corridorEnemy.DeactivateEnemy();
        StartCoroutine(FadeOutRenderer());
    }

    private bool IsPlayerLookingAtThis()
    {
        Vector3 dir = (_player.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(dir, transform.forward);
        Debug.Log("Player offset looking at barnacle " + dot);
        return dot >= PlayerLookAtOffsetTolerance;
    }

    private bool IsPlayerWithinDistance()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        Debug.Log("Distance to player from barnacle is " + distance);
        return distance <= PlayerWithinRadius;
    }

    private IEnumerator FadeOutRenderer()
    {
        float lerpStart_Time = Time.time;
        float lerpProgress;
        bool lerping = true;
        while (lerping)
        {
            yield return new WaitForEndOfFrame();
            lerpProgress = Time.time - lerpStart_Time;
            if (_renderer != null)
            {
                _renderer.material.color = Color.Lerp(_originalColor, _fadedColor, lerpProgress / FadingDurationSeconds);
            }
            else
            {
                lerping = false;
            }


            if (lerpProgress >= FadingDurationSeconds)
            {
                lerping = false;
                OnObjectedFaded();
            }
        }
        yield break;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scenes.Common.Scripts;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] float SecondsDelayUntilInputAcceptance = 4f;
    private MarkSceneManager markSceneManager;
    private float _timeUntilInputAcceptance = 0.0f;

    void Start()
    {
        markSceneManager = FindObjectOfType<MarkSceneManager>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if ( Time.unscaledTime > _timeUntilInputAcceptance) // if we allow input after a delay 
        {
            if ( Input.GetButtonDown("A Button"))
            {
                RestartScene();
            }
        }
    }

    void OnEnable()
    {
        Time.timeScale = 0f;
        _timeUntilInputAcceptance = Time.unscaledTime + SecondsDelayUntilInputAcceptance; // add a delay
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void RestartScene()
    {
        markSceneManager.RestartCurrentScene();
    }

}

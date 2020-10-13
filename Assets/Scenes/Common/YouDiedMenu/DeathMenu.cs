using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scenes.Common.Scripts;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] float SecondsDelayUntilInputAcceptance = 4f;
    [SerializeField] GameObject ButtonToFocus;
    [SerializeField] AudioSource AudioToPlayWhenDead;
    private MarkSceneManager markSceneManager;
    private float _timeUntilInputAcceptance = 0.0f;

    void Start()
    {
        markSceneManager = FindObjectOfType<MarkSceneManager>();
    }

    void OnEnable()
    {
        Time.timeScale = 0f;
        _timeUntilInputAcceptance = Time.unscaledTime + SecondsDelayUntilInputAcceptance; // add a delay
        EventSystem.current.SetSelectedGameObject(ButtonToFocus);
        if (AudioToPlayWhenDead != null )
        {
            AudioToPlayWhenDead.Play();
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private void HideDeathMenu()
    {
        gameObject.SetActive(false);
    }

    public void RestartScene()
    {
        if (Time.unscaledTime > _timeUntilInputAcceptance) // if we allow input after a delay 
        {
            HideDeathMenu();
            markSceneManager.RestartCurrentScene();
        }
    }

}

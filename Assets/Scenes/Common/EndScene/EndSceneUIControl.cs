using Assets.Scenes.Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class EndSceneUIControl : MonoBehaviour
{
    [SerializeField] int NextSceneToLoad;
    [SerializeField] float SecondsDelayUntilInputAcceptance = 2f;
    [SerializeField] GameObject ButtonToFocus;
    [SerializeField] AudioSource AudioToPlayOnActivation;
    [SerializeField] GameObject[] GameObjectsToDeactivate;
    [SerializeField] MarkRelic Relic;
    [SerializeField] MarkSceneManager MarkSceneManager;
    private float _timeUntilInputAcceptance = 0.0f;

    void Start()
    {
        Assert.IsNotNull(MarkSceneManager);
    }

    void OnEnable()
    {
        MarkSceneManager.IsSceneCompleted = true;
        MarkGameState.AddCollectedRelic(Relic);
        MarkSceneManager.StopAllAudio();
        Time.timeScale = 0f;
        _timeUntilInputAcceptance = Time.unscaledTime + SecondsDelayUntilInputAcceptance; // add a delay
        EventSystem.current.SetSelectedGameObject(ButtonToFocus);
        if (AudioToPlayOnActivation != null)
        {
            AudioToPlayOnActivation.Play();
        }
        if ( GameObjectsToDeactivate != null )
        {
            GameObjectsToDeactivate.ToList().ForEach(go => go.SetActive(false)); // deactivate each one
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

    private void HideMenu()
    {
        gameObject.SetActive(false);
    }

    public void NextScene()
    {
        if (Time.unscaledTime > _timeUntilInputAcceptance) // if we allow input after a delay 
        {
            HideMenu();
            MarkSceneManager.LoadScene(NextSceneToLoad);
        }
    }
}

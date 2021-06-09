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
    [SerializeField] GameObject TimeTrialUI;
    [SerializeField] TMPro.TextMeshProUGUI TimeTrialText;

    private float _timeUntilInputAcceptance = 0.0f;

    void Start()
    {
        Assert.IsNotNull(MarkSceneManager);
    }

    void OnEnable()
    {
        MarkStopWatch.Stop(); 
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
            GameObjectsToDeactivate.ToList().FindAll(go => go != null).ForEach(go => go.SetActive(false)); // deactivate each one
        }
        ShouldShowTimeTrial();
    }

    private void Update()
    {
        if (Input.GetButtonDown("X Button"))
        {
            MarkGameState.IsShowingTimeTrial = !MarkGameState.IsShowingTimeTrial;
            ShouldShowTimeTrial();
        }
    }

    private void ShouldShowTimeTrial()
    {
        if ( TimeTrialUI != null )
        {
            TimeTrialUI.SetActive(MarkGameState.IsShowingTimeTrial);
            if ( TimeTrialUI.activeSelf && TimeTrialText != null)
            {
                Debug.Log("Time taken to complete this level: " +MarkStopWatch.Elapsed());
                TimeTrialText.text = MarkStopWatch.Elapsed();
            }
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

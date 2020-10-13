using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scenes.Common.Scripts;

public class MarksPauseMenu : MonoBehaviour
{
    private bool IsGamePaused;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] Button ResumeButton;
    [SerializeField] GameObject GameObjectWithMarkSceneManager;
    [SerializeField] GameObject DebugOptions;
    [SerializeField] Dropdown sceneSelectDebugDropdown;
    [SerializeField] bool AllowPauseMenu = true;

    private MarkSceneManager markSceneManager;

    void Start()
    {
        markSceneManager = GameObjectWithMarkSceneManager.GetComponent<MarkSceneManager>();
        Resume(); // simulate a resume on start
    }

    void OnEnable()
    {
        DebugOptions.SetActive(MarkGameState.IsInDebugMode);
        if (MarkGameState.IsInDebugMode)
        {
            sceneSelectDebugDropdown.onValueChanged.AddListener(delegate { sceneSelectDebugDropdownValueChangedHandler(sceneSelectDebugDropdown); });
        }
        EventSystem.current.SetSelectedGameObject(ResumeButton.gameObject); // set the resume button as the default selection
    }

    void OnDisable()
    {
        sceneSelectDebugDropdown.onValueChanged.RemoveAllListeners();
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null); // remove the current selected item
        }
    }

    private void sceneSelectDebugDropdownValueChangedHandler(Dropdown target)
    {
        int sceneSelected = target.value;
        Debug.Log("sceneSelected: " + sceneSelected);
        markSceneManager.LoadScene(sceneSelected);
    }

    // Update is called once per frame
    void Update()
    {
        if ( !AllowPauseMenu )
        {
            return;
        }

        if ( Input.GetButtonDown("Menu Button") )
        {
            Debug.Log("Menu button pressed");
            if (IsGamePaused)
            {
                Resume(); // we are unpausing on the pause menu
            } else
            {
                Pause();
            }
        }

        if ( IsGamePaused )
        {
            if (Input.GetButtonDown("Y Button"))
            {
                MarkGameState.IsInDebugMode = !MarkGameState.IsInDebugMode; // flip the setting
            }

            if (Input.GetButtonDown("B Button"))
            {
                Resume();
            }
        }

        
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false); // don't show the menu
        Time.timeScale = 1f; // unfreeze the game
        IsGamePaused = false;
    }

    private void Pause()
    {
        PauseMenuUI.SetActive(true); // set the menu to show
        Time.timeScale = 0f; // freeze the game
        IsGamePaused = true;
    }

    public void LoadMainMenu()
    {
        markSceneManager.LoadScene(0);
    }

    public void RestartScene()
    {
        markSceneManager.RestartCurrentScene();
    }

}

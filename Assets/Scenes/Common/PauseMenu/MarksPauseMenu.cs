using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MarksPauseMenu : MonoBehaviour
{
    private bool IsGamePaused;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] Button ResumeButton;
    [SerializeField] GameObject GameObjectWithMarkSceneManager;
    [SerializeField] Dropdown sceneSelectDebugDropdown;
    [SerializeField] bool AllowPauseMenu = true;

    private MarkSceneManager markSceneManager;

    void Start()
    {
        markSceneManager = GameObjectWithMarkSceneManager.GetComponent<MarkSceneManager>();
        sceneSelectDebugDropdown.onValueChanged.AddListener(delegate {
            sceneSelectDebugDropdownValueChangedHandler(sceneSelectDebugDropdown);
        });
        Resume(); // simulate a resume on start
    }

    void Destroy()
    {
        sceneSelectDebugDropdown.onValueChanged.RemoveAllListeners();
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
        if ( Input.GetButtonDown("B Button") && IsGamePaused)
        {
            Resume();
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
        ResumeButton.gameObject.SetActive(true);
        ResumeButton.Select();
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

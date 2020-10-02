using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MarksPauseMenu : MonoBehaviour
{
    private bool IsGamePaused;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] Button ResumeButton;

    void Start()
    {
        Resume(); // simulate a resume on start
    }

    // Update is called once per frame
    void Update()
    {

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
        Time.timeScale = 1f; // unfreeze the game if frozen
        SceneManager.LoadScene(0); // main menu is at root
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restart the scene
    }
}

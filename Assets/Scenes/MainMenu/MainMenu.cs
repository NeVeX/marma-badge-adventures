using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject FocusButton;

    public void StartGame()
    {
        Debug.Log("Starting the game!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // next scene please
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(FocusButton);
    }

    void OnDisable()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null); // remove the current selected item
        }
    }

    public void QuitGame()
    {
        Debug.LogWarning("Quitting the game!");
        //if (Application.isEditor) // Don't use this code, it won't build/deploy with it in
        //{
        //    UnityEditor.EditorApplication.isPlaying = false; // stop playing if we are in the editor
        //}
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{    
    public void StartGame()
    {
        Debug.Log("Starting the game!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // next scene please
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

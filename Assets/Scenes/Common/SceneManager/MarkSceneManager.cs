﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarkSceneManager : MonoBehaviour
{
    [SerializeField] GameObject LoadingScreenPanel;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int buildIndex)
    {
        Debug.Log("Loading scene " + buildIndex);
        ShowLoadingScreen();
        SceneManager.LoadScene(buildIndex); // main menu is at root
    }

    public void LoadNextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartCurrentScene()
    {
        Debug.Log("Restarting current scene");
        LoadScene(SceneManager.GetActiveScene().buildIndex); // restart the scene
    }

    private void ShowLoadingScreen()
    {
        LoadingScreenPanel.SetActive(true);
        Time.timeScale = 0f; // unfreeze the game if frozen
        AudioListener.pause = true;
    }

    private void Reset()
    {
        LoadingScreenPanel.SetActive(false);
        Time.timeScale = 1f; // unfreeze the game if frozen
        AudioListener.pause = false;
    }

}
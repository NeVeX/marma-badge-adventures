using Assets.Scenes.Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarkSceneManager : MonoBehaviour
{
    [SerializeField] GameObject LoadingScreenPanel;
    [HideInInspector] public bool IsSceneCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    public void StopAllAudio()
    {
        var audioSources = FindObjectsOfType<AudioSource>();
        if (audioSources != null && audioSources.Length > 0)
        {
            Debug.Log("Stopping all audiosources");
            audioSources.ToList().ForEach(aso => aso.Stop());
        } else
        {
            Debug.Log("No Audiosources to stop");
        }
    }

    public void LoadScene(int buildIndex)
    {
        MarkGameState.PreviousSceneLoaded = GetCurrentSceneIndex();
        Debug.Log("Loading scene " + buildIndex);
        ShowLoadingScreen();
        SceneManager.LoadScene(buildIndex); // main menu is at root
    }

    public void LoadNextScene()
    {
        LoadScene(GetCurrentSceneIndex() + 1);
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void RestartCurrentScene()
    {
        // MarkGameState.ShowIntroSceneMessage = false;
        Debug.Log("Restarting current scene");
        LoadScene(GetCurrentSceneIndex()); // restart the scene
    }

    public bool IsInRestartedScene()
    {
        return GetCurrentSceneIndex() == MarkGameState.PreviousSceneLoaded;
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

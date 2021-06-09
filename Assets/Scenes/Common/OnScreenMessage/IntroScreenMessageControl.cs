using Assets.Scenes.Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreenMessageControl : MonoBehaviour
{
    [SerializeField] bool ShowMessageOnRestartedScene = false;
    [SerializeField] ScreenMessageControl StartupScreenMessageControl;
    [SerializeField] ScreenMessageControl AlternativeStartupScreenMessageControl;

    private MarkSceneManager _markSceneManager;

    void Start()
    {
        MarkStopWatch.Restart();
        _markSceneManager = GameObject.FindObjectOfType<MarkSceneManager>();
        int currentScene = _markSceneManager.GetCurrentSceneIndex();
        bool isInRestartedScene = _markSceneManager.IsInRestartedScene();
        if ( !isInRestartedScene || (isInRestartedScene && ShowMessageOnRestartedScene)) 
        {
            // have we already shown this startup message
            if ( MarkGameState.HasShownStartupMessageForScene(currentScene))
            {
                AlternativeStartupScreenMessageControl.ShowMessage();
                Debug.Log("Showing alternative intro message on screen");
            } else
            {
                MarkGameState.AddStartupMessageShownForScene(currentScene);
                StartupScreenMessageControl.ShowMessage();
                Debug.Log("Showing intro message on screen");
            }
        } else
        {
            Debug.Log("Not showing any intro message on screen");
        }
    }
}

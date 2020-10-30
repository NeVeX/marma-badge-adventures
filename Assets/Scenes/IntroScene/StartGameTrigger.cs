using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTrigger : MonoBehaviour
{
    [SerializeField] float WaitSecondsUntilInputAllowed = 20f;
    [SerializeField] GameObject GameObjectWithMarkSceneManager;
    private MarkSceneManager markSceneManager;
    private float _startTime;

    private void Start()
    {
        markSceneManager = GameObjectWithMarkSceneManager.GetComponent<MarkSceneManager>();
        _startTime = Time.time + WaitSecondsUntilInputAllowed;
    }

    // Update is called once per frame
    void Update()
    {
        bool canAcceptInput = Time.time > _startTime;
        if (canAcceptInput && Input.GetButtonDown("A Button") )
        {
            //int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            markSceneManager.LoadNextScene();
            //Debug.Log("Ending the scene and loading next scene [" + nextScene + "]");
            //SceneManager.LoadScene(nextScene);
        }   
    }
}

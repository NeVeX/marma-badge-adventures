using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTrigger : MonoBehaviour
{
    [SerializeField] GameObject GameObjectWithMarkSceneManager;
    private MarkSceneManager markSceneManager;

    private void Start()
    {
        markSceneManager = GameObjectWithMarkSceneManager.GetComponent<MarkSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetButtonDown("A Button") )
        {
            //int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            markSceneManager.LoadNextScene();
            //Debug.Log("Ending the scene and loading next scene [" + nextScene + "]");
            //SceneManager.LoadScene(nextScene);
        }   
    }
}

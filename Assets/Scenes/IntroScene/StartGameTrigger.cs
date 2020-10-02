using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameTrigger : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetButtonDown("A Button") )
        {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            Debug.Log("Ending the scene and loading next scene [" + nextScene + "]");
            SceneManager.LoadScene(nextScene);
        }   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEndingTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log("Ending the scene and loading next scene ["+nextScene+"]");
        SceneManager.LoadScene(nextScene); 
    }
}

using Assets.Scenes.Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TowerCollision : MonoBehaviour
{
    [SerializeField] GameObject MessageCannotEnterPanel; // no such thing as "panel" in scripts
    [SerializeField] int SceneToLoadOnEntry;
    private ScreenMessageControl ScreenMessageControl;
    private MarkSceneManager _markSceneManager; 

    private void Start()
    {
        ScreenMessageControl = MessageCannotEnterPanel.transform.parent.GetComponent<ScreenMessageControl>();
        _markSceneManager = Object.FindObjectOfType<MarkSceneManager>();
        Assert.IsNotNull(_markSceneManager);
    }

    void OnPlayerColliderHit()
    {
        Debug.Log("Player collision with tower entrance");
        // Check if have all relics
        if (MarkGameState.AreAllRelicsCollected())
        {
            _markSceneManager.LoadScene(SceneToLoadOnEntry);
        } else
        {
            ScreenMessageControl.ShowMessage();
        }
    }

}

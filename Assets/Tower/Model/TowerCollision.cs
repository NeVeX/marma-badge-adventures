using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCollision : MonoBehaviour
{
    [SerializeField] GameObject MessageCannotEnterPanel; // no such thing as "panel" in scripts
    private ScreenMessageControl ScreenMessageControl;

    private void Start()
    {

        ScreenMessageControl = MessageCannotEnterPanel.transform.parent.GetComponent<ScreenMessageControl>();
        //IsShowingMessage = false;
        //MessageCannotEnterPanel.SetActive(IsShowingMessage);
    }

    void OnPlayerColliderHit()
    {
        Debug.Log("Player collision with tower entrance");
        ScreenMessageControl.ShowMessage();
    }

}

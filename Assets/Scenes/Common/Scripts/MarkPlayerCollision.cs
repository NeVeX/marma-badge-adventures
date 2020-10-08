using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkPlayerCollision : MonoBehaviour
{
    // capture you died screen and reset 
    [SerializeField] GameObject DeathMenu;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string gameObjectTag = hit.gameObject.tag;
        if (gameObjectTag.Equals("Untagged"))
        {
            return;
        }
        Debug.Log("Player collided with something via OnControllerColliderHit - " + gameObjectTag);
        OnPlayerHit(gameObjectTag);
        hit.transform.SendMessage("OnPlayerColliderHit", SendMessageOptions.DontRequireReceiver);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        string gameObjectTag = other.gameObject.tag;
        if (gameObjectTag.Equals("Untagged"))
        {
            return;
        }
        Debug.Log("Player collided with something via OnTriggerEnter - " + gameObjectTag);
        OnPlayerHit(gameObjectTag);
    }

    private void OnPlayerHit(string tag)
    {
        switch (tag)
        {
            case "CorridorEnemy":
                OnCorridorEnemyHit();
                break;
        }
    }

    private void OnCorridorEnemyHit()
    {
        Debug.Log("Game over for the player - hit by corridor enemy!");
        DeathMenu.SetActive(true);
    }
}

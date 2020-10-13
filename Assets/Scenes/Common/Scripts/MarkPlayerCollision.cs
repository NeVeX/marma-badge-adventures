using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkPlayerCollision : MonoBehaviour
{
    // capture you died screen and reset 
    [SerializeField] GameObject DeathMenu;
    private enum CollisionType { CharacterController, RigidBody }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string gameObjectTag = hit.gameObject.tag;
        if (gameObjectTag.Equals("Untagged"))
        {
            return;
        }
        //Debug.Log("Player collided with something via OnControllerColliderHit - " + gameObjectTag);
        OnPlayerHit(gameObjectTag, CollisionType.CharacterController);
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
        OnPlayerHit(gameObjectTag, CollisionType.RigidBody);
    }

    private void OnPlayerHit(string tag, CollisionType collisionType)
    {
        switch (tag)
        {
            case "CorridorEnemy":
                if (collisionType == CollisionType.RigidBody)
                {
                    OnCorridorEnemyHit(); // only take damage from "things" hitting the player
                }
                break;
        }
    }

    private void OnCorridorEnemyHit()
    {
        Debug.Log("Game over for the player - hit by corridor enemy!");
        DeathMenu.SetActive(true);
    }
}

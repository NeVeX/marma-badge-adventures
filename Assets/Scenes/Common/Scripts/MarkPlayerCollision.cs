using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkPlayerCollision : MonoBehaviour
{

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if ( hit.gameObject.tag.Equals("Untagged"))
        {
            return;
        }


        //if (hit.transform.tag == "SomeTag")
        //{
        //    hit.transform.SendMessage("SomeFunction", SendMessageOptions.DontRequireReceiver);
        //}
        hit.transform.SendMessage("OnPlayerColliderHit", SendMessageOptions.DontRequireReceiver);
        Debug.Log("Player collided with something - " + hit.gameObject.tag);
    }
}

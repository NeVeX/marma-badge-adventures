using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndSceneTrigger : MonoBehaviour
{
    [SerializeField] GameObject UIToShow;

    public void OnPlayerColliderHit()
    {
        UIToShow.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("pickup"))
        {
            UIToShow.SetActive(true);
        }
    }

}

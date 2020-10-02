using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCrawler : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 20f;

    [SerializeField] float timeToStartScrollingSeconds = 5f;

    void Update()
    {
        // Don't scroll for a few seconds
        if ( Time.timeSinceLevelLoad < timeToStartScrollingSeconds) { return; }  

        // Scroll the text
        Vector3 pos = transform.position;
        Vector3 localVectorUp = transform.TransformDirection(0, 1, 0);
        pos += localVectorUp * scrollSpeed * Time.deltaTime;
        transform.position = pos;
    }
}

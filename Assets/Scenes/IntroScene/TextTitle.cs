using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextTitle : MonoBehaviour
{
    // TODO: better gradual speed up

    [SerializeField] float textBackwardsSpeed = 10f;
    [SerializeField] float speedUpMultiplier = 1.2f;
    [SerializeField] float timeInSecondsToSpeedUp = 3f;
    [SerializeField] float timeInSecondsToFadeText = 10f;

    private bool hasSpedUpAlready = false;
    private bool isTextFaded = false;
    private TextMesh text;

    private void Start()
    {
        text = GetComponent<TextMesh>();
    }

    void Update()
    {
       
        Vector3 pos = transform.position;
        if (!isTextFaded && Time.timeSinceLevelLoad > timeInSecondsToFadeText)
        {
            isTextFaded = false;
            //fadeTextOut(text);
            //StartCoroutine(FadeTextToZeroAlpha(3f, text));
            StartCoroutine(fadeTextOut2(text));
        }
        if ( !hasSpedUpAlready && Time.timeSinceLevelLoad > timeInSecondsToSpeedUp)
        {
            textBackwardsSpeed *= speedUpMultiplier;
            hasSpedUpAlready = true;
        }

        // Move text back
        Vector3 localVectorBack = transform.TransformDirection(0, 0, 1);
        pos += localVectorBack * textBackwardsSpeed * Time.deltaTime;
        transform.position = pos;
    }

    private bool fadeTextOut(TextMesh text)
    {
        Color colorOfObject = text.color;
        float prop = (Time.time / 3.0f);
        colorOfObject.a = Mathf.Lerp(1, 0, prop);
        text.color = colorOfObject;
        return text.color.a == 0;
    }

    private IEnumerator fadeTextOut2(TextMesh textMesh)
    {
        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= (Time.deltaTime / 3f))
        {
            // set color with i as alpha
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, i);
            yield return null;
        }
    }

    //private IEnumerator FadeTextToZeroAlpha(float time, TextMesh text)
    //{
    //    print("Starting to fade text");
    //    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
    //    while (text.color.a > 0.0f)
    //    {
    //        print("Starting to fade text - "+text.color.a);
    //        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / time));
    //        yield return null;
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    private Light _light;
    private float dayIntensity;
    private float nightIntensity;
    [SerializeField] float dayLengthInSeconds;
    private float changeIntensity;
    private float startIntensity;
    private float endIntensity;

    void Start()
    {
        // We assume it starts at day time
        this._light = GetComponent<Light>();
        this.dayIntensity = this._light.intensity;
        this.nightIntensity = 0.0f;
        this.changeIntensity = 0.0f;
        this.startIntensity = this.dayIntensity;
        this.endIntensity = this.nightIntensity;

    }

    void Update()
    {
        changeIntensity += Time.deltaTime / dayLengthInSeconds;
        _light.intensity = Mathf.Lerp(startIntensity, endIntensity, changeIntensity);
        if ( changeIntensity >= 1.0f)
        {
            //swap the values and restart (going the other way)
            float temp = endIntensity;
            endIntensity = startIntensity;
            startIntensity = temp;
            changeIntensity = 0.0f;
        }
        
    }
}

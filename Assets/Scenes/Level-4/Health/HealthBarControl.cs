using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControl : MonoBehaviour
{
    [SerializeField] RectTransform HealthBar;
    private float _healthBarMax;
    private float _maxHealth;

    private void Start()
    {
        _healthBarMax = HealthBar.rect.width;
    }

    public void SetMaxHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void SetHealth(float health)
    {
        float newWidth = _healthBarMax / (_maxHealth / health);
        HealthBar.sizeDelta = new Vector2(newWidth, HealthBar.sizeDelta.y);
        //Debug.Log("Health bar update. HealthBarMax: " + _healthBarMax + ", maxHealth: " + _maxHealth + ", newHealth: " + newWidth);
    }

}

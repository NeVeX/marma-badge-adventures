using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MarkHUDScript : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI HealthAmountText;

    private MarkPlayerCollision _markPlayerCollision;

    private void Start()
    {
        _markPlayerCollision = GameObject.Find("Player").GetComponent<MarkPlayerCollision>();
    }
    private void Update()
    {
        updateHealthAmount();
    }

    private void updateHealthAmount()
    {
        int health = Mathf.CeilToInt(_markPlayerCollision.GetPlayerHealthRemaining());
        Color color = Color.green;
        if ( health < 25 )
        {
            color = Color.red;
        }
        else if ( health < 60 )
        {
            color = Color.yellow;
        }
        HealthAmountText.color = color;
        HealthAmountText.text = health.ToString();
    }
}

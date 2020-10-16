using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkDamageObject : MonoBehaviour
{
    [SerializeField] float Damage;

    public float GetDamageAmount()
    {
        return Damage;
    }
}

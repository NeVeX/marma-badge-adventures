using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkChicken : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Animator>().Play("Run In Place-Mark");
    }
}

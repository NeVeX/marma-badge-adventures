using UnityEngine;
using System.Collections;

public class Badger : MonoBehaviour {
    Animator badger;
    private IEnumerator coroutine;
	// Use this for initialization
	void Start () {
        badger = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            badger.SetBool("idle", true);
            badger.SetBool("eat", false);
            badger.SetBool("sniff", false);
            badger.SetBool("walk", false);
            badger.SetBool("turnright", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("trot", false);
            badger.SetBool("trotleft", false);
            badger.SetBool("trotright", false);
            badger.SetBool("growl", false);
            badger.SetBool("threat", false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            badger.SetBool("eat", true);
            badger.SetBool("idle", false);
            badger.SetBool("sniff", false);
            badger.SetBool("walk", false);
            badger.SetBool("turnright", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("trot", false);
            badger.SetBool("trotleft", false);
            badger.SetBool("trotright", false);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            badger.SetBool("sniff", true);
            badger.SetBool("idle", false);
            badger.SetBool("eat", false);
            badger.SetBool("walk", false);
            badger.SetBool("turnright", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("trot", false);
            badger.SetBool("trotleft", false);
            badger.SetBool("trotright", false);
        }
        if (Input.GetKey("down"))
        {
            badger.SetBool("walk", true);
            badger.SetBool("idle", false);
            badger.SetBool("sniff", false);
            badger.SetBool("eat", false);
            badger.SetBool("turnright", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("run", false);
            badger.SetBool("runleft", false);
            badger.SetBool("runright", false);
            badger.SetBool("trot", false);
            badger.SetBool("trotleft", false);
            badger.SetBool("trotright", false);
            badger.SetBool("sit", false);
            badger.SetBool("growl", false);
            badger.SetBool("threat", false);
        }
        if (Input.GetKey("left"))
        {
            badger.SetBool("turnleft", true);
            badger.SetBool("turnright", false);
            badger.SetBool("walk", false);
            badger.SetBool("idle", false);
            badger.SetBool("eat", false);
            badger.SetBool("sniff", false);
            badger.SetBool("run", false);
            badger.SetBool("runleft", false);
            badger.SetBool("runright", false);
            badger.SetBool("trot", false);
            badger.SetBool("trotleft", false);
            badger.SetBool("trotright", false);
            badger.SetBool("sit", false);
            badger.SetBool("growl", false);
            badger.SetBool("threat", false);
        }
        if (Input.GetKey("right"))
        {
            badger.SetBool("turnright", true);
            badger.SetBool("turnleft", false);
            badger.SetBool("walk", false);
            badger.SetBool("idle", false);
            badger.SetBool("eat", false);
            badger.SetBool("sniff", false);
            badger.SetBool("run", false);
            badger.SetBool("runleft", false);
            badger.SetBool("runright", false);
            badger.SetBool("trot", false);
            badger.SetBool("trotleft", false);
            badger.SetBool("trotright", false);
            badger.SetBool("sit", false);
            badger.SetBool("growl", false);
            badger.SetBool("threat", false);
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            badger.SetBool("run", true);
            badger.SetBool("runleft", false);
            badger.SetBool("runright", false);
            badger.SetBool("walk", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("turnright", false);
            badger.SetBool("jump", false);
            badger.SetBool("trot", false);
            badger.SetBool("trotleft", false);
            badger.SetBool("trotright", false);
            badger.SetBool("threat", false);
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            badger.SetBool("runleft", true);
            badger.SetBool("runright", false);
            badger.SetBool("run", false);
            badger.SetBool("walk", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("turnright", false);
            badger.SetBool("jump", false);
            badger.SetBool("trot", false);
            badger.SetBool("trotleft", false);
            badger.SetBool("trotright", false);
            badger.SetBool("growl", false);
            badger.SetBool("threat", false);
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            badger.SetBool("runright", true);
            badger.SetBool("runleft", false);
            badger.SetBool("run", false);
            badger.SetBool("walk", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("turnright", false);
            badger.SetBool("jump", false);
            badger.SetBool("trot", false);
            badger.SetBool("trotleft", false);
            badger.SetBool("trotright", false);
            badger.SetBool("threat", false);
        }
        if (Input.GetKey(KeyCode.Keypad8))
        {
            badger.SetBool("jump", true);
            badger.SetBool("walk", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("turnright", false);
            badger.SetBool("run", false);
            badger.SetBool("runleft", false);
            badger.SetBool("runright", false);
            badger.SetBool("trot", false);
            badger.SetBool("trotleft", false);
            badger.SetBool("trotright", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            badger.SetBool("trot", true);
            badger.SetBool("trotleft", false);
            badger.SetBool("trotright", false);
            badger.SetBool("walk", false);
            badger.SetBool("turnright", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("run", false);
            badger.SetBool("runleft", false);
            badger.SetBool("runright", false);
            badger.SetBool("idle", false);
            badger.SetBool("eat", false);
            badger.SetBool("sniff", false);
            badger.SetBool("jump", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            badger.SetBool("trotleft", true);
            badger.SetBool("trotright", false);
            badger.SetBool("trot", false);
            badger.SetBool("walk", false);
            badger.SetBool("turnright", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("run", false);
            badger.SetBool("runleft", false);
            badger.SetBool("runright", false);
            badger.SetBool("idle", false);
            badger.SetBool("eat", false);
            badger.SetBool("sniff", false);
            badger.SetBool("jump", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            badger.SetBool("trotright", true);
            badger.SetBool("trotleft", false);
            badger.SetBool("trot", false);
            badger.SetBool("walk", false);
            badger.SetBool("turnright", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("run", false);
            badger.SetBool("runleft", false);
            badger.SetBool("runright", false);
            badger.SetBool("idle", false);
            badger.SetBool("eat", false);
            badger.SetBool("sniff", false);
            badger.SetBool("jump", false);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            badger.SetBool("sitdown", true);
            badger.SetBool("idle", false);
            badger.SetBool("walk", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("turnright", false);
            StartCoroutine("sit");
            sit();
        }
        if (Input.GetKey(KeyCode.W))
        {
            badger.SetBool("scratch", true);
            badger.SetBool("sit", false);
            StartCoroutine("sitafterscratch");
            sitafterscratch();
        }
        if (Input.GetKey(KeyCode.E))
        {
            badger.SetBool("getup", true);
            badger.SetBool("sit", false);
            StartCoroutine("idle");
            idle();
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            badger.SetBool("growl", true);
            badger.SetBool("idle", false);
            badger.SetBool("threat", false);
            badger.SetBool("walk", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("turnright", false);
            badger.SetBool("run", false);
            badger.SetBool("runleft", false);
            badger.SetBool("runright", false);
        }
        if (Input.GetKey(KeyCode.Alpha0))
        {
            badger.SetBool("threat", true);
            badger.SetBool("idle", false);
            badger.SetBool("growl", false);
            badger.SetBool("walk", false);
            badger.SetBool("turnleft", false);
            badger.SetBool("turnright", false);
            badger.SetBool("run", false);
            badger.SetBool("runleft", false);
            badger.SetBool("runright", false);
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            badger.SetBool("attack", true);
            badger.SetBool("threat", false);
            StartCoroutine("threat");
            threat();
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            badger.SetBool("hit", true);
            badger.SetBool("growl", false);
            StartCoroutine("growl");
            growl();
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            badger.SetBool("die", true);
            badger.SetBool("growl", false);
            badger.SetBool("hit", false);
        }
    }
    IEnumerator sit()
    {
        yield return new WaitForSeconds(0.4f);
        badger.SetBool("sitdown", false);
        badger.SetBool("sit", true);
    }
    IEnumerator sitafterscratch()
    {
        yield return new WaitForSeconds(2.0f);
        badger.SetBool("scratch", false);
        badger.SetBool("sit", true);
    }
    IEnumerator idle()
    {
        yield return new WaitForSeconds(0.4f);
        badger.SetBool("getup", false);
        badger.SetBool("idle", true);
    }
    IEnumerator threat()
    {
        yield return new WaitForSeconds(0.7f);
        badger.SetBool("attack", false);
        badger.SetBool("threat", true);
    }
    IEnumerator growl()
    {
        yield return new WaitForSeconds(0.8f);
        badger.SetBool("hit", false);
        badger.SetBool("growl", true);
    }
}

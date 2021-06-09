using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarkDoor : MonoBehaviour
{
    [SerializeField] AudioSource DoorOpeningSound;
    [SerializeField] int SceneNumber;
    [SerializeField] float SecondsForDoorOpen = 5.0f;
    [SerializeField] GameObject BlackDoor;
    [SerializeField] GameObject MessagePanel;
    [SerializeField] GameObject GameObjectWithCharacterController;

    private Animator Animator;
    private ScreenMessageControl ScreenMessageControl;
    private CharacterController CharacterController;

    private void Start()
    {
        Animator = gameObject.transform.parent.GetComponent<Animator>();
        if (MessagePanel != null)
        {
            ScreenMessageControl = MessagePanel.transform.parent.GetComponent<ScreenMessageControl>();
        }
        CharacterController = GameObjectWithCharacterController.GetComponent<CharacterController>();
        BlackDoor.SetActive(false);
        Time.timeScale = 1;
    }

    void OnPlayerColliderHit()
    {
        Debug.Log("Player collision a door");
        if (ScreenMessageControl != null)
        {
            ScreenMessageControl.ShowMessage(OnMessageAnswer);
        } else
        {
            OpenDoor();
        }
    }

    private void OnMessageAnswer(ScreenMessageControl.MessageAnswer messageAnswer)
    {
        switch(messageAnswer)
        {
            case ScreenMessageControl.MessageAnswer.Yes:
                OpenDoor();
                break;
            case ScreenMessageControl.MessageAnswer.No:
                CloseDoor();
                break;
        }
    }


    private void OpenDoor()
    {
        Animator.SetBool("open", true); //open door
        DoorOpeningSound.Play(); // play the sound
        BlackDoor.SetActive(true);
        Debug.Log("Opening door to load next level " + SceneNumber);
        CharacterController.enabled = false;
        StartCoroutine(LoadNextSceneAfterSeconds(SecondsForDoorOpen));
    }

    private IEnumerator LoadNextSceneAfterSeconds(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        SceneManager.LoadScene(SceneNumber);
    }

    private void CloseDoor()
    {
        //Animator.SetBool("open", false); //close door
        Debug.Log("Closing (not opening) the door to load next level " + SceneNumber);
    }

}

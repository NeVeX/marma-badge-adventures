using Assets.Scenes.Common.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMessageControl : MonoBehaviour
{

    public enum MessageAnswer { Yes, No }

    private bool _IsMessageShowing;
    public bool IsMessageShowing
    {
        get { return _IsMessageShowing; }
        private set {
            _IsMessageShowing = value;
            MarkGameState.IsOnScreenMessageShowing = _IsMessageShowing;
            Debug.Log("IsOnScreenMessageShowing -> " + _IsMessageShowing);
        }
    }

    [SerializeField] GameObject MessagePanel;
    [SerializeField] GameObject MessagePanelAfter;
    [SerializeField] AudioSource PlayerInputYesSound;
    [SerializeField] GameObject CharacterControllerOnGameObject;
    [SerializeField] bool ShouldFreezeControllerWhileShowing = false;
    [SerializeField] bool ShowOnStartup = true;
    [SerializeField] bool MessageShowUntilPlayerInput = true;
    [SerializeField] int MessageShowForSeconds = 5;

    private AudioSource AudioSource;
    private CharacterController _characterController;
    private List<Action<MessageAnswer>> MessageAnswerListeners = new List<Action<MessageAnswer>>();

    void Start()
    {
        SetMessageActive(MessagePanel, ShowOnStartup);
        AudioSource = MessagePanel.GetComponent<AudioSource>();
        _characterController = CharacterControllerOnGameObject.GetComponent<CharacterController>(); // can be null
      
    }

    // Update is called once per frame
    void Update()
    {
        bool aButton = Input.GetButtonDown("A Button");
        bool bButton = Input.GetButtonDown("B Button");
        if ( IsMessageShowing && (aButton || bButton) )
        {
            HideMessage(MessagePanel);
            if (aButton)
            {
                MessageAnswerRecieved(MessageAnswer.Yes);
            } else if (bButton)
            {
                MessageAnswerRecieved(MessageAnswer.No);
            }
        } 
    }

    public void ShowMessage(Action<MessageAnswer> answerCallBack = null)
    {
        // TODO: Check if player has all relics - this would be cool to see actually happen
        if (!IsMessageShowing)
        {
            ShowMessage(MessagePanel);
            if (answerCallBack != null )
            {
                MessageAnswerListeners.Add(answerCallBack);
            }
            if (!MessageShowUntilPlayerInput)
            {
                StartCoroutine(SetInactiveAfterSeconds(MessagePanel, MessageShowForSeconds));
            }
        }
    }

    private void MessageAnswerRecieved(MessageAnswer messageAnswer)
    {
        switch (messageAnswer)
        {
            case MessageAnswer.Yes:
                PlayerInputYesSound.Play();
                break;
            case MessageAnswer.No:
                // nothing right now...
                break;
        }
        // Now invoke any listeners
        MessageAnswerListeners.ForEach(action => action(messageAnswer));
        MessageAnswerListeners.Clear();
    }
       
    private IEnumerator SetInactiveAfterSeconds(GameObject panel, int secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        HideMessage(panel);
    }

    private void ShowMessage(GameObject panel)
    {
        SetMessageActive(panel, true);
        if (AudioSource != null)
        {
            AudioSource.Play();
        }
    }

    private void HideMessage(GameObject panel)
    {
        SetMessageActive(panel, false);
        if ( MessagePanelAfter != null )
        {
            Debug.Log("Showing MessagePanelAfter hiding this panel");
            MessagePanelAfter.transform.parent.GetComponent<ScreenMessageControl>().ShowMessage();
        } else
        {
            Debug.Log("There is no MessagePanelAfter to show after hiding this panel");
        }
    }

    private void SetMessageActive(GameObject panel, bool isActive)
    {
        IsMessageShowing = isActive;
        panel.SetActive(IsMessageShowing);
        CharacterControllerEnable(!isActive);
    }

    private void CharacterControllerEnable(bool enabled)
    {
        if ( _characterController != null )
        {
            _characterController.enabled = enabled;
        }
    }
}

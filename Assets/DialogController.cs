using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public struct Message
{
    public Message(string t, float d)
    {
        text = t;
        delay = d;
    }

    public string text { get;  }
    public float delay { get; }
}

public class DialogController : MonoBehaviour
{

    [SerializeField] GameObject _messageText;
    [SerializeField] Image _textBoxImage;
    Color _textBoxImageColor;

    LinkedList<Message> _messageList = new LinkedList<Message>();

    float _nextTextTime = 0;
    bool _textFading = false;

    // Start is called before the first frame update
    void Start()
    {
        _textBoxImageColor = _textBoxImage.color;
        _messageList.AddFirst(new Message("It is good to get a sound impression of your surroundings. But I think you'll get to see this room a couple of times anyway.", 2f));
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI text = _messageText.GetComponent<TextMeshProUGUI>();
        if (_nextTextTime <= Time.time && !_textFading && _messageList.Count != 0)
        {
            StopCoroutine("WaitForFadingEnd");
            text.alpha = 1f;
            _textBoxImage.color = _textBoxImageColor;
            Debug.Log("ShowMessage");
            Message currentMessage = _messageList.First.Value;
            Debug.Log(currentMessage);
            _messageList.RemoveFirst();
            UpdateText(currentMessage.text);

            //Activates Fading of Text after a certain delay
            _nextTextTime = Time.time + currentMessage.delay;
            _textFading = true;
        }
        else if (_nextTextTime <= Time.time && _textFading)
        {
            Debug.Log("ShowMessage Else");
            text.CrossFadeAlpha(0.0f, 1.0f, false);
            _textBoxImage.CrossFadeAlpha(0.0f, 1.0f, false);
            StartCoroutine("WaitForFadingEnd");
        }
        /*else {
            Debug.Log("else");
        }*/
    }

    IEnumerator WaitForFadingEnd()
    {
        yield return new WaitForSeconds(1.0f);
        _textFading = false;
    }

    private void LateUpdate()
    {
        TextMeshProUGUI text = _messageText.GetComponent<TextMeshProUGUI>();
        if(text.alpha == 0.0f)
        {
            _textFading = false;
        }
    }

    public void QueueText(Message message)
    {
        _messageList.AddLast(message);
    }

    public void StackText(Message message)
    {
        _messageList.AddFirst(message);
    }

    void UpdateText(string message)
    {
        TextMeshProUGUI text = _messageText.GetComponent<TextMeshProUGUI>();
        text.text = message;
        _textBoxImage.color = _textBoxImageColor;
        Debug.Log("UpdateText: " + message);
    }

    public void TriggerFallMessage()
    {

    }

    public void HandleTrigger(string triggerName)
    {
        switch (triggerName)
        {
            case "HammerFall":
                HammerFallTrigger();
                break;
            case "PlattformFall":
                PlattformFallTrigger();
                break;
            case "HammerRoomFall":
                HammerRoomFallTrigger();
                break;
            case "RotationRoom1Fall":
                RotationRoom1FallTrigger();
                break;
            case "RotationRoom2Fall":
                RotationRoom2FallTrigger();
                break;
            case "Completion":
                CompletionTrigger();
                break;
            case "EndPortal":
                EndPortalTrigger();
                break;
            case "StartRoom":
                StartRoomTrigger();
                break;
            default: break;
        }
    }

    List<Message> _StartroomMessages = new List<Message>()
    {
        new Message("Sometimes we fail and the only way forward is to start all over again", 2f),
        new Message("Isn't it comforting to get back to a familiar place?", 2f),
        new Message("Do you feel alone? After all there is nobody here, besides me.", 2f),
        new Message("Frustration is something we all feel from time to time. Are you frustrated yet?", 2f),
        new Message("Even though we all feel frustrated, it is not a good guide to archive a goal.  What is your goal anyway? Do you know why you're trying again and again, just to keep failing?", 2f),
        new Message("And yet another time back to where you started.", 2f),

    };
    private void StartRoomTrigger()
    {

        if (_StartroomMessages.Count != 0)
        {
            Debug.Log("Trigger Startroom Message");
            Message message = _StartroomMessages.ElementAt(0);
            _StartroomMessages.RemoveAt(0);
            _messageList.AddFirst(message);
        }
    }

    private void CompletionTrigger()
    {
        throw new NotImplementedException();
    }

    private void EndPortalTrigger()
    {
        throw new NotImplementedException();
    }

    private void RotationRoom2FallTrigger()
    {
        throw new NotImplementedException();
    }

    private void RotationRoom1FallTrigger()
    {
        throw new NotImplementedException();
    }

    private void HammerRoomFallTrigger()
    {
        throw new NotImplementedException();
    }

    private void PlattformFallTrigger()
    {
        throw new NotImplementedException();
    }

    private void HammerFallTrigger()
    {
        throw new NotImplementedException();
    }
}

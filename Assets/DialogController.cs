using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.SceneManagement;
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
    float _textBoxImageAlpha;

    LinkedList<Message> _messageList = new LinkedList<Message>();

    float _nextTextTime = 0;
    bool _textFading = false;

    // Start is called before the first frame update
    void Start()
    {
        _textBoxImageAlpha = _textBoxImage.color.a;
        //_messageList.AddFirst(new Message("It is good to get a sound impression of your surroundings. But I think you'll get to see this room a couple of times anyway.", 2f));
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI text = _messageText.GetComponent<TextMeshProUGUI>();
        if (_nextTextTime <= Time.time && !_textFading && _messageList.Count != 0)
        {
            StopCoroutine("WaitForFadingEnd");
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
        text.CrossFadeAlpha(255f, 0.5f, false);
        _textBoxImage.CrossFadeAlpha(_textBoxImageAlpha, 0.2f, false);
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
            case "HammerRoomCompletion":
                HammerRoomCompletion();
                break;
            default: break;
        }
    }

    List<Message> _StartroomMessages = new List<Message>()
    {
        new Message("It is good to get a sound impression of your surroundings. But I think you'll get to see this room a couple of times anyway.", 2f),
        new Message("Sometimes we fail and the only way forward is to start all over again", 2f),
        new Message("Isn't it comforting to get back to a familiar place?", 2f),
        new Message("Do you feel alone? After all there is nobody here, besides me.", 2f),
        new Message("Frustration is something we all feel from time to time. Are you frustrated yet?", 2f),
        new Message("Even though we all feel frustrated, it is not a good guide to archive a goal.  What is your goal anyway? Do you know why you're trying again and again, just to keep failing?", 3f),
        new Message("And yet another time back to where you started.", 2f),

    };



    private bool _startRoomMessageToggle = true;
    private void StartRoomTrigger()
    {

        if (_StartroomMessages.Count != 0 && _startRoomMessageToggle)
        {
            _startRoomMessageToggle = false;
            Message message = _StartroomMessages.ElementAt(0);
            _StartroomMessages.RemoveAt(0);
            _messageList.AddFirst(message);
        }
        else
        {
            _startRoomMessageToggle = true;
        }
    }

    List<Message> _HammerFallMessages = new List<Message>()
    {
        new Message("How unfortunate to fall. Next time you will surly make it. It isn't to hard.", 2f),
        new Message("Sometimes the task seems simple, but we still fail.", 2f),
        new Message("What would you like to hear? Encouragement? Repriment?", 2f),
    };

    private void HammerFallTrigger()
    {
        if (_HammerFallMessages.Count != 0)
        {
            Message message = _HammerFallMessages.ElementAt(0);
            _HammerFallMessages.RemoveAt(0);
            _messageList.AddLast(message);
        }
    }


    private bool _completionFlag = false;
    private void CompletionTrigger()
    {
        if (!_completionFlag)
        {
            _completionFlag = true;
            _messageList.AddFirst(new Message("Your perseverance is truly impressive. I think you can be proud of yourself for overcoming your last obstacle.", 3f));
        }
    }

    private bool _hammerRoomCompletionFlag = false;
    private void HammerRoomCompletion()
    {
        if (!_hammerRoomCompletionFlag)
        {
            _hammerRoomCompletionFlag = true;
            _messageList.AddFirst(new Message("It's not difficult at all, if you just know the correct door.", 2f));
        }
    }

    private void EndPortalTrigger()
    {
        _StartroomMessages = new List<Message>();
        _messageList.AddLast(new Message("Although you made it, in the end all your effords are in vain.", 2f));
        _messageList.AddLast(new Message("You didn't archive anything. Now you just are back to the beginning. But did you expect otherwise?", 2f));

    }


    List<Message> _RotationRoom2FallMessages = new List<Message>()
    {
        new Message("You made it further than before.", 2f),
        new Message("Just a little bit until you reach the other side.", 2f),
        new Message("Why do you keep trying?", 2f),
        new Message("There is nothing more to say, if you don't want to give up.", 2f),
    };
    private void RotationRoom2FallTrigger()
    {
        if (_RotationRoom2FallMessages.Count != 0)
        {
            Message message = _RotationRoom2FallMessages.ElementAt(0);
            _RotationRoom2FallMessages.RemoveAt(0);
            _messageList.AddLast(message);
        }
    }

    List<Message> _RotationRoom1FallMessages = new List<Message>()
    {
        new Message("You surpassed the obstacles up to now. This one will be the last, but also the most difficult.", 2f),
        new Message("I know falling hurts. Especially since the way back up is so long.", 2f),
        new Message("What do you think? How many times do you have to try? Can you even make it?", 2f),
        new Message("You can always take the easy way out.", 2f),
        new Message("So you didn't just quit?", 2f),
    };

    private void RotationRoom1FallTrigger()
    {
        if (_RotationRoom1FallMessages.Count != 0)
        {
            Message message = _RotationRoom1FallMessages.ElementAt(0);
            _RotationRoom1FallMessages.RemoveAt(0);
            _messageList.AddLast(message);
        }
    }

    private void HammerRoomFallTrigger()
    {
        throw new NotImplementedException();
    }

    private void PlattformFallTrigger()
    {
        throw new NotImplementedException();
    }

}

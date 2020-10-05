using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
//using UnityEditor.SceneManagement;
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
    float _textBoxImageAlpha;

    LinkedList<Message> _messageList = new LinkedList<Message>();

    float _nextTextTime = 0;
    bool _textFading = false;

    // Start is called before the first frame update
    void Start()
    {
        _textBoxImageColor = _textBoxImage.color;
        _textBoxImageAlpha = _textBoxImage.color.a;
        _messageList.AddFirst(new Message("It is good to get a sound impression of your surroundings. But I think you'll get to see this room a couple of times anyway.", 2f));
    }

    float _fadeOutEndTime = 0f;
    bool _fadeOutTimeSet = false;

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI text = _messageText.GetComponent<TextMeshProUGUI>();
        if (_nextTextTime <= Time.time && !_textFading && _messageList.Count != 0)
        {
            //StopCoroutine("WaitForFadingEnd");
            Message currentMessage = _messageList.First.Value;
            _messageList.RemoveFirst();
            UpdateText(currentMessage.text);

            //Activates Fading of Text after a certain delay
            _nextTextTime = Time.time + currentMessage.delay;
            _textFading = true;
            _fadeOutEndTime = _nextTextTime + 1.8f;
        }
        /*else if(_textFading && _nextTextTime <= Time.time && !_fadeOutTimeSet)
        {
            _fadeOutEndTime = Time.time + 0.5f;
            _fadeOutTimeSet = true;
        }*/
        else if(_nextTextTime <= Time.time && _textFading && _fadeOutEndTime >= Time.time)
        {
            text.CrossFadeAlpha(0.0f, 0.5f, false);
            _textBoxImage.CrossFadeAlpha(0.0f, 0.5f, false);
        }
        else if (_textFading && _fadeOutEndTime <= Time.time)
        {
            _textFading = false;
        }
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
        text.CrossFadeAlpha(255f, 1f, false);
        //_textBoxImage.color = _textBoxImageColor;
        _textBoxImage.CrossFadeAlpha(_textBoxImageAlpha, 0.2f, false);
    }

    public void TriggerFallMessage()
    {

    }


    bool _showPortalText = true;
    public void HandleTrigger(string triggerName)
    {
        if (triggerName.Contains("Fall") && _showPortalText && !triggerName.Equals("HammerRoomFall"))
        {
            StartRoomTrigger();
            _showPortalText = false;
        }
        else
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
                case "HammerRoomCompletion":
                    HammerRoomCompletion();
                    break;
                case "PlattformPush":
                    PlattformPush();
                    break;
                case "PlattformDrop":
                    PlattformDrop();
                    break;
                default: break;
            }
            if (triggerName.Contains("Fall"))
            {
                _showPortalText = true;
            }
        }
    }

    private bool _plattformDropToggle = true;
    private void PlattformDrop()
    {
        if (_plattformDropToggle)
        {
            _plattformDropToggle = false;
            _messageList.AddFirst(new Message("There are many unstable things around. Sometimes it is even the floor, we try to stand on.", 2f));
        }
    }

    private bool _plattformPushToggle = true;
    private void PlattformPush()
    {
        if (_plattformPushToggle)
        {
            _plattformPushToggle = false;
            _messageList.AddFirst(new Message("Other times we just get pushed down.", 2f));
        }
    }

    List<Message> _StartroomMessages = new List<Message>()
    {
        new Message("I hope you don't mind me watching you. Although you can try to ignore me.", 2f),
        new Message("Sometimes we fail and the only way forward is to start all over again.", 2f),
        new Message("All you see is grey. At least there is something to see.", 2f),
        new Message("Do you feel alone? After all, there is nobody here, besides me.", 2f),
        new Message("Isn't it comforting to get back to a familiar place?", 2f),
        new Message("Frustration is something we all feel from time to time. Are you frustrated yet?", 2f),
        new Message("Even though we all feel frustrated, it is not a good guide to achieve a goal.  What is your goal anyway? Do you know why you try again and again, just to keep failing?", 3f),
        new Message("Do you know the two types of frustration?", 2f),
        new Message("Internal frustration comes from within. It is the result of your inability to achieve your self imposed goals.", 3f),
        new Message("External frustration is caused by circumstances that are out of your control. Hazards on your way, put there by other people.", 3f),
        new Message("I thought about it. Internal or external, knowing the difference doesn't change anything.", 2f),
        new Message("And yet another time back to where you started.", 2f),
        new Message("If you're honest, there isn't a reason to keep trying. Just stop and lie down. You don't have the energy to keep going.", 3f),

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
        new Message("How unfortunate to fall. Next time you will surely make it. It isn't too hard.", 2f),
        new Message("The task seems simple, but we still fail.", 2f),
        new Message("What would you like to hear? Encouragement? There is no need for encouragement, when someone is as hopeless as you.", 2f),
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
        _PlattformFallMessages = new List<Message>();
        _HammerFallMessages = new List<Message>();
        _HammerRoomFallMessages = new List<Message>();
        _RotationRoom1FallMessages = new List<Message>();
        _RotationRoom2FallMessages = new List<Message>();

        _messageList.AddLast(new Message("Although you made it, in the end all your efforts are in vain.", 2f));
        _messageList.AddLast(new Message("You didn't achieve anything. Now you just are back to the beginning. But did you expect otherwise?", 2f));
        _messageList.AddLast(new Message("Will you continue the cycle or do you decide to leave everything behind?", 2f));
    }


    List<Message> _RotationRoom2FallMessages = new List<Message>()
    {
        new Message("You made it further than before. That's a good thing.", 2f),
        new Message("Just a little bit until you reach the other side.", 2f),
        new Message("Maybe you can make it.", 2f),
        new Message("You gave me hope, but now you're failing again.", 2f),
        new Message("Do you even deserve to make it to the end?", 2f),
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
        new Message("You've surpassed the obstacles up to now. This one will be the last, but also the most difficult.", 2f),
        new Message("I know falling hurts. Especially since the way back up is so long.", 2f),
        new Message("What do you think? How many times do you have to try? Can you even make it?", 2f),
        new Message("You can always take the easy way out.", 2f),
        new Message("So you didn't just quit?", 2f),
        new Message("You surely can't make it.", 1f),
        new Message("I'm sure other people enjoy what they're doing. Do you still know what it means to enjoy something?", 2f),
        new Message("Are you at a point, where you don't even know what to do anymore?", 2f),
        new Message("You drag yourself back up just to continue to suffer.", 2f),
        new Message("Life isn't fair. It never has been.", 2f),

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

    List<Message> _HammerRoomFallMessages = new List<Message>()
    {
        new Message("How unfair. This isn't a question of skill. There is nothing you can do to better the outcome.", 2f),
        new Message("Now there is only one option left.", 2f),
        new Message("Some say it is madness to do the same thing, while expecting a different result.", 2f),
    };
    private void HammerRoomFallTrigger()
    {
        if (_HammerRoomFallMessages.Count != 0)
        {
            Message message = _HammerRoomFallMessages.ElementAt(0);
            _HammerRoomFallMessages.RemoveAt(0);
            _messageList.AddLast(message);
        }
    }

    List<Message> _PlattformFallMessages = new List<Message>()
    {
        new Message("Maybe it is easier to rush through and leave the problem behind.", 2f),
        new Message("With a little bit of practise you may get what you want every time.", 2f),
        new Message("Why don't you learn from your mistakes? The right path is directly in front of you?", 2f),
    };
    private void PlattformFallTrigger()
    {
        if (_PlattformFallMessages.Count != 0 && _messageList.Count == 0)
        {
            Message message = _PlattformFallMessages.ElementAt(0);
            _PlattformFallMessages.RemoveAt(0);
            _messageList.AddLast(message);
        }
    }

}

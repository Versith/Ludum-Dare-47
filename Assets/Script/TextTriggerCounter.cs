using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTriggerCounter : TextTrigger
{
    public int _countUntilTrigger;
    public string _text;
    public int _messageDelay;

    private int _counter;
    private bool _isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && _isActive)
        {
            _isActive = false;
            _counter += 1;
            if(_counter == _countUntilTrigger)
            {
                _dialogController.QueueText(new Message(_text, _messageDelay));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _isActive = true;
        }
    }
}

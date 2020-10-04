using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] private Queue<string> _texts = new Queue<string>();
    [SerializeField] private Queue<float> _delays = new Queue<float>();

    protected DialogController _dialogController;

    protected void Start()
    {
        _dialogController = GameObject.FindObjectOfType<DialogController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _dialogController.QueueText(new Message(_texts.Dequeue(), _delays.Dequeue()));
        }
    }
}

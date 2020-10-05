using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] private string _triggerName;
    private bool _triggerActive = true;

    protected DialogController _dialogController;

    protected void Start()
    {
        _dialogController = GameObject.FindObjectOfType<DialogController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && _triggerActive)
        {
            _triggerActive = false;
            StopCoroutine("ResetActive");
            StartCoroutine("ResetActive");
            _dialogController.HandleTrigger(_triggerName);
        }
    }

    IEnumerator ResetActive()
    {
        yield return new WaitForSeconds(2f);
        _triggerActive = true;
    }
}

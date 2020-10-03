using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToPlatform : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    private bool isAttached = false;

    private void Update()
    {
        bool attachedNow = false;

        Collider[] hits = Physics.OverlapSphere(_groundCheck.position, _checkRadius, _groundMask);
        foreach(Collider hit in hits)
        {
            if(hit.CompareTag("Platform"))
            {
                transform.SetParent(hit.transform);
                isAttached = true;
                attachedNow = true;
            }
        }

        if(!attachedNow)
        {
            transform.SetParent(null);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    [SerializeField] private Transform _teleportPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = _teleportPoint.position;
            other.transform.rotation = _teleportPoint.rotation;
        }
    }
}

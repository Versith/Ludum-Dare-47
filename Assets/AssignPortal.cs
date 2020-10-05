using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPortal : MonoBehaviour
{
    [SerializeField] private Transform _portal;
    [SerializeField] private Transform _teleportMarker;

    private GameObject exitPortal;

    private void Awake()
    {
        exitPortal = GameObject.FindGameObjectWithTag("ExitPortal");
    }

    private void Start()
    {
        exitPortal.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _portal.localPosition = _teleportMarker.position;
            _portal.rotation = _teleportMarker.rotation;

            if (exitPortal.activeSelf == false)
            {
                exitPortal.SetActive(true);
            }
        }
    }
}

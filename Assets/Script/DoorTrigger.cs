using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _master;

    private bool _active = true;

    private void OnTriggerEnter(Collider other)
    {
        if(_active)
        {
            _active = false;
            _master.GetComponent<DoorMaster>().TriedDoor(this.gameObject);
        }
    }
}

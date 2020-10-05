using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMaster : MonoBehaviour
{
    [SerializeField] private List<GameObject> _triggers;

    public void TriedDoor(GameObject door)
    {
        _triggers.Remove(door);
        if(_triggers.Count == 1)
        {
            _triggers[0].SetActive(false);
        }
    }
}

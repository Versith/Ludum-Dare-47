using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructTrigger : MonoBehaviour
{
    [SerializeField] private Transform _object;

    private Construct _construct;

    private void Start()
    {
        _construct = _object.GetComponent<Construct>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _construct.StartConstruct();
    }
}

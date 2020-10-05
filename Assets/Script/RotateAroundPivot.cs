using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPivot : MonoBehaviour, Construct
{
    [SerializeField] private float _speed = 1;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.MoveRotation(Quaternion.Euler(0, transform.eulerAngles.y + _speed * Time.deltaTime, 90));
    }

    public void StartConstruct()
    {
        
    }

    public void ResetConstruct()
    {

    }
}

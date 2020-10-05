using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour, Construct
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _degreesPerSecond;
    [SerializeField] private float _pushStrength;
    [SerializeField] private float _delay = 0f;
    [SerializeField] private bool _active = false;

    private float _yRotation;    
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private float _windUp;

    // Start is called before the first frame update
    void Start()
    {
        _yRotation = transform.eulerAngles.y;
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _windUp = Time.time + _delay;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_active && Time.time > _windUp)
        {
            if (other.CompareTag("Player"))
            {
                Vector3 dir = new Vector3(-Mathf.Sin(Mathf.Deg2Rad * _yRotation), 0, -Mathf.Cos(Mathf.Deg2Rad * _yRotation));
                other.GetComponentInParent<PlayerMovement>().GetPushed(dir, _pushStrength);
            }
        }
        else if(!_active)
        {
            _windUp = Time.time + _delay;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_active && Time.time > _windUp)
        {
            transform.RotateAround(_pivot.position, transform.right, _degreesPerSecond * Time.deltaTime);
        }
    }

    public void ResetConstruct()
    {
        _active = false;
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
    }

    public void StartConstruct()
    {
        _active = true;
    }
}

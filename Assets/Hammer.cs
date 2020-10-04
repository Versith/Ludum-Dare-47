using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour, Construct
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _degreesPerSecond;
    [SerializeField] private float _pushStrength;
    [SerializeField] private bool _active = false;

    private float _yRotation;    
    private Transform _initialTransform;

    // Start is called before the first frame update
    void Start()
    {
        _yRotation = transform.eulerAngles.y;
        _initialTransform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_active)
        {
            if (other.CompareTag("Player"))
            {
                Vector3 dir = new Vector3(-Mathf.Sin(Mathf.Deg2Rad * _yRotation), 0, -Mathf.Cos(Mathf.Deg2Rad * _yRotation));
                other.GetComponentInParent<PlayerMovement>().GetPushed(dir, _pushStrength);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_active)
        {
            transform.RotateAround(_pivot.position, transform.right, _degreesPerSecond * Time.deltaTime);
        }
    }

    public void ResetConstruct()
    {
        _active = false;
        transform.position = _initialTransform.position;
        transform.rotation = _initialTransform.rotation;
        transform.localScale = _initialTransform.localScale;
    }

    public void StartConstruct()
    {
        _active = true;
    }
}

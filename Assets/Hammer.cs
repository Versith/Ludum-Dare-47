using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _degreesPerSecond;
    [SerializeField] private float _pushStrength;

    private float _yRotation;

    // Start is called before the first frame update
    void Start()
    {
        _yRotation = transform.eulerAngles.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 dir = new Vector3(-Mathf.Sin(Mathf.Deg2Rad * _yRotation), 0, -Mathf.Cos(Mathf.Deg2Rad * _yRotation));
            other.GetComponentInParent<PlayerMovement>().GetPushed(dir, _pushStrength);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(_pivot.position, transform.right, _degreesPerSecond * Time.deltaTime);
    }
}

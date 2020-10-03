using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 12f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    private CharacterController _controller;

    private Vector3 _velocity;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _controller = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if(isGrounded && _velocity.y < 0)
        {
            _velocity.y = -1f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * _moveSpeed * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }
}

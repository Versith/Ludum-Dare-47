using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 12f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _coyoteTime = 0.1f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    private CharacterController _controller;

    private Vector3 _velocity;
    private bool isGrounded = false;
    private float _lastPlatformExit;

    private bool isPushed = false;
    private Vector3 _pushDirection;
    private float _pushForce;

    // Start is called before the first frame update
    void Start()
    {
        _controller = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if(isGrounded)
        {
            _lastPlatformExit = Time.time;
        }

        if(isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        if(isPushed)
        {
            _controller.Move(_pushDirection * _pushForce * Time.deltaTime);
            _pushForce -= 1;
            if(_pushForce <= 0)
            {
                isPushed = false;
            }
        }
        else
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            _controller.Move(move * _moveSpeed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump") && (Time.time < _lastPlatformExit + _coyoteTime))
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    public void GetPushed(Vector3 direction, float force)
    {
        isPushed = true;
        _pushDirection = direction;
        _pushForce = force;
    }
}

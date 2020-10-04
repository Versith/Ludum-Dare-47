using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 12f;
    [SerializeField] private float _slowDownPercentage = 1f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _jumpBufferLength = 0.1f;
    [SerializeField] private float _coyoteTime = 0.1f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    private CharacterController _controller;

    private Vector3 _velocity;
    private float _currentSpeed;
    private bool isGrounded = false;
    private float _lastPlatformExit;
    private float _baseSlopeLimit;

    private float _jumpBuffer;

    private bool isPushed = false;
    private Vector3 _pushDirection;
    private float _pushForce;

    // Start is called before the first frame update
    void Start()
    {
        _controller = transform.GetComponent<CharacterController>();
        _baseSlopeLimit = _controller.slopeLimit;
        _currentSpeed = _moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if(isGrounded)
        {
            _lastPlatformExit = Time.time;
            _controller.slopeLimit = _baseSlopeLimit;

            if(Time.time < _jumpBuffer)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
                _controller.slopeLimit = 90;
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
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

            _controller.Move(move * _currentSpeed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump"))
        {
            if(Time.time < _lastPlatformExit + _coyoteTime)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
                _controller.slopeLimit = 90;
            }
            else
            {
                _jumpBuffer = Time.time + _jumpBufferLength;
            }

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

    public void ResetSpeed()
    {
        _currentSpeed = _moveSpeed;
    }

    public void Slower()
    {
        _currentSpeed *= _slowDownPercentage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour, Construct
{
    [SerializeField] private Transform _pathParent;
    [SerializeField] private string _behaviour; // bounce, loop, stop
    [SerializeField] private float _delay = 0;
    [SerializeField] private float _speed = 1;
    [SerializeField] private bool _active = false;

    private Transform[] _path;
    private Transform[] _initialPath;
    private Transform _initialTransform;
    private int _currentNode = 0;
    private float _progress = 0;
    private bool isFinished = false;
    private float _windUp;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] tempPath = _pathParent.GetComponentsInChildren<Transform>();
        _path = new Transform[tempPath.Length - 1];
        _initialPath = new Transform[tempPath.Length - 1];
        System.Array.Copy(tempPath, 1, _path, 0, tempPath.Length - 1);
        System.Array.Copy(tempPath, 1, _initialPath, 0, tempPath.Length - 1);

        _initialTransform = transform;
        _windUp = Time.time + _delay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_active && Time.time > _windUp)
        {
            if (_behaviour == "Loop")
            {
                _progress += _speed * Time.deltaTime;
                if (_currentNode + 1 >= _path.Length)
                {
                    transform.position = Vector3.Lerp(_path[_currentNode].position, _path[0].position, _progress);
                    transform.rotation = Quaternion.Lerp(_path[_currentNode].rotation, _path[0].rotation, _progress);
                    transform.localScale = Vector3.Lerp(_path[_currentNode].localScale, _path[0].localScale, _progress);
                }
                else
                {
                    transform.position = Vector3.Lerp(_path[_currentNode].position, _path[_currentNode + 1].position, _progress);
                    transform.rotation = Quaternion.Lerp(_path[_currentNode].rotation, _path[_currentNode + 1].rotation, _progress);
                    transform.localScale = Vector3.Lerp(_path[_currentNode].localScale, _path[_currentNode + 1].localScale, _progress);
                }
                if (_progress >= 1)
                {
                    _progress = 0;
                    _currentNode++;
                    if (_currentNode >= _path.Length)
                    {
                        _currentNode = 0;
                    }
                }

            }
            else if (_behaviour == "Bounce")
            {
                _progress += _speed * Time.deltaTime;

                transform.position = Vector3.Lerp(_path[_currentNode].position, _path[_currentNode + 1].position, _progress);
                transform.rotation = Quaternion.Lerp(_path[_currentNode].rotation, _path[_currentNode + 1].rotation, _progress);
                transform.localScale = Vector3.Lerp(_path[_currentNode].localScale, _path[_currentNode + 1].localScale, _progress);

                if (_progress >= 1)
                {
                    _progress = 0;
                    _currentNode++;
                    if (_currentNode >= _path.Length - 1)
                    {
                        _currentNode = 0;
                        System.Array.Reverse(_path);
                    }
                }
            }
            else if (_behaviour == "Stop" && !isFinished)
            {
                _progress += _speed * Time.deltaTime;
                if (!(_currentNode + 1 >= _path.Length))
                {
                    transform.position = Vector3.Lerp(_path[_currentNode].position, _path[_currentNode + 1].position, _progress);
                    transform.rotation = Quaternion.Lerp(_path[_currentNode].rotation, _path[_currentNode + 1].rotation, _progress);
                    transform.localScale = Vector3.Lerp(_path[_currentNode].localScale, _path[_currentNode + 1].localScale, _progress);
                }
                if (_progress >= 1)
                {
                    _progress = 0;
                    _currentNode++;
                    if (_currentNode >= _path.Length)
                    {
                        isFinished = true;
                        _currentNode = 0;
                        ResetConstruct();
                    }
                }
            }
        }
        else if(!_active)
        {
            _windUp = Time.time + _delay;
        }

    }

    public void ResetConstruct()
    {
        _active = false;
        _currentNode = 0;
        _progress = 0;
        isFinished = false;
        _path = _initialPath;
        transform.position = _initialTransform.position;
        transform.rotation = _initialTransform.rotation;
        transform.localScale = _initialTransform.localScale;
    }

    public void StartConstruct()
    {
        _active = true;
    }
}

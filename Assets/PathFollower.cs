using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private Transform _pathParent;
    [SerializeField] private string _behaviour; // bounce, loop, stop

    private Transform[] _path;
    private int _currentNode = 0;
    private float _progress = 0;
    private bool isFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] tempPath = _pathParent.GetComponentsInChildren<Transform>();
        _path = new Transform[tempPath.Length - 1];
        System.Array.Copy(tempPath, 1, _path, 0, tempPath.Length - 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(_behaviour == "Loop")
        {
            _progress += Time.deltaTime;
            if (_currentNode + 1 >= _path.Length)
            {
                transform.position = Vector3.Lerp(_path[_currentNode].position, _path[0].position, _progress);
                transform.rotation = Quaternion.Lerp(_path[_currentNode].rotation, _path[0].rotation, _progress);
            }
            else
            {
                transform.position = Vector3.Lerp(_path[_currentNode].position, _path[_currentNode + 1].position, _progress);
                transform.rotation = Quaternion.Lerp(_path[_currentNode].rotation, _path[_currentNode + 1].rotation, _progress);
            }
            if(_progress >= 1)
            {
                _progress = 0;
                _currentNode++;
                if(_currentNode >= _path.Length)
                {
                    _currentNode = 0;
                }
            }

        }
        else if(_behaviour == "Bounce")
        {
            _progress += Time.deltaTime;

            transform.position = Vector3.Lerp(_path[_currentNode].position, _path[_currentNode + 1].position, _progress);
            transform.rotation = Quaternion.Lerp(_path[_currentNode].rotation, _path[_currentNode + 1].rotation, _progress);

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
        else if(_behaviour == "Stop" && !isFinished)
        {
            _progress += Time.deltaTime;
            if(!(_currentNode + 1 >= _path.Length))
            {
                transform.position = Vector3.Lerp(_path[_currentNode].position, _path[_currentNode + 1].position, _progress);
                transform.rotation = Quaternion.Lerp(_path[_currentNode].rotation, _path[_currentNode + 1].rotation, _progress);
            }
            if (_progress >= 1)
            {
                _progress = 0;
                _currentNode++;
                if (_currentNode >= _path.Length)
                {
                    isFinished = true;
                    _currentNode = 0;
                }
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructController : MonoBehaviour
{
    [SerializeField] private GameObject[] _constructs;
    [SerializeField] private bool[] _startAgain;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            int i = 0;
            foreach(GameObject c in _constructs)
            {
                c.GetComponent<Construct>().ResetConstruct();
                if(_startAgain[i])
                {
                    c.GetComponent<Construct>().StartConstruct();
                }
                i++;
            }
        }
    }
}

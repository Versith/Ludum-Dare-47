using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAwayCharacter : MonoBehaviour
{
    [SerializeField] private float _pushStrength = 50f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") && collision.collider.GetComponent<PlayerMovement>() != null)
        {
            Vector3 normal = collision.GetContact(0).normal;
            normal = new Vector3(-normal.x, 0, -normal.z);
            collision.collider.GetComponent<PlayerMovement>().GetPushed(normal, _pushStrength);
        }
    }
}

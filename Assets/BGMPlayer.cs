using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource source = transform.GetComponent<AudioSource>();
        source.clip = _clip;
        source.Play();
    }
}

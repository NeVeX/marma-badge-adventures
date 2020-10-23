using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkExplosion : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private AudioSource _audioSource;

    public void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
    }

    public float Explode()
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
        if ( _particleSystem != null )
        {
            _particleSystem.Play();
            return _particleSystem.main.duration;
        }
        return 0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkAudioLoopingPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] _audioClips;
    private AudioSource _audioSource;
    private int _nextClipIndex;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _nextClipIndex = 0;
        //StartCoroutine(PlayAudio(0));
    }

    private void Update()
    {
        if ( !_audioSource.isPlaying)
        {
            // play next clip
            AudioClip audioClip = _audioClips[_nextClipIndex];
            _audioSource.clip = audioClip;
            _audioSource.Play();
            
            _nextClipIndex++; // move to next clip
            if (_nextClipIndex >= _audioClips.Length)
            {
                _nextClipIndex = 0; // restart
            }
        }
    }

    //private IEnumerator PlayAudio(int index)
    //{
        
    //    yield return new WaitForSeconds(_audioSource.clip.length);
    //    // Play the next 
    //    int nextIndex = index + 1;
        
    //    StartCoroutine(PlayAudio(nextIndex));
    //}
}

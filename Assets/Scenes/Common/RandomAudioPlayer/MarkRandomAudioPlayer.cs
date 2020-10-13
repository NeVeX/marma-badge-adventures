using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkRandomAudioPlayer : MonoBehaviour
{
    [SerializeField] float EveryHowManySeconds = 45f;
    [SerializeField] float MinimumBetweenPlaySeconds = 15f;
    [SerializeField] AudioClip[] AudioClips;
    [SerializeField] AudioSource RandomAudioSource;
    private float _timeTillNextRandom;

    // Start is called before the first frame update
    void Start()
    {
        RandomAudioSource = gameObject.GetComponent<AudioSource>();
        SetNextRandomPlayTime(0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioClips == null) { return; }
        if (!RandomAudioSource.isPlaying && Time.unscaledTime > _timeTillNextRandom)
        {
            float soundLength = PlayRandom();
            SetNextRandomPlayTime(soundLength);
        }
    }

    float PlayRandom()
    {
        RandomAudioSource.clip = AudioClips[Random.Range(0, AudioClips.Length)];
        RandomAudioSource.Play();
        return RandomAudioSource.clip.length;
    }

    private void SetNextRandomPlayTime(float soundLength)
    {
        float minSoundLength = Mathf.Max(MinimumBetweenPlaySeconds, soundLength);
        _timeTillNextRandom = Time.unscaledTime + minSoundLength + Random.Range(0, EveryHowManySeconds);
    }

}

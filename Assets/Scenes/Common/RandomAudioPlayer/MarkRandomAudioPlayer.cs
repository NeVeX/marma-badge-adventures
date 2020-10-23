using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class MarkRandomAudioPlayer : MonoBehaviour
{
    //[SerializeField] float EveryHowManySeconds = 45f;
    [SerializeField] float MinimumBetweenPlaySeconds = 15f;
    [SerializeField] AudioClip[] AudioClips;
    [SerializeField] AudioSource RandomAudioSource;
    [SerializeField] bool IsManualControlled = false;
    
    private MarkSceneManager _markSceneManager;
    private float _timeTillNextRandom = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _markSceneManager = Object.FindObjectOfType<MarkSceneManager>();
        Assert.IsNotNull(_markSceneManager);
        RandomAudioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioClips == null || AudioClips.Length == 0) { return; }
        if (IsManualControlled) { return; }
        PlayRandomIfAllowed();
    }

    public float PlayRandomIfAllowed()
    {
        if (!RandomAudioSource.isPlaying && Time.unscaledTime > _timeTillNextRandom && !_markSceneManager.IsSceneCompleted)
        {
            float soundLength = PlayRandomNow();
            SetNextRandomPlayTime(soundLength);
            return soundLength;
        }
        return 0.0f;
    }

    public float PlayRandomNow()
    {
        RandomAudioSource.clip = AudioClips[Random.Range(0, AudioClips.Length)];
        RandomAudioSource.Play();
        return RandomAudioSource.clip.length;
    }

    public void StopPlaying()
    {
        RandomAudioSource.Stop();
        SetNextRandomPlayTime(float.MaxValue); // none will ever play again
    }

    private void SetNextRandomPlayTime(float soundLength)
    {
        // float minSoundLength = Mathf.Max(MinimumBetweenPlaySeconds, soundLength);
        _timeTillNextRandom = Time.unscaledTime + soundLength + MinimumBetweenPlaySeconds;
    }

}

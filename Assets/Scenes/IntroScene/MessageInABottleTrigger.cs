using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MessageInABottleTrigger : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    private MeshRenderer meshRenderer;
    private AudioSource audioSource;
    [SerializeField] AudioClip recordScratch;
    [SerializeField] AudioClip messageInABottle;
    private bool isAlreadyVisible = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if ( isAlreadyVisible ) { return; }

        bool isVisible = GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(mainCamera), meshRenderer.bounds);
        if (isVisible)
        {
            isAlreadyVisible = true;
            StartCoroutine(playMessageInABottle());
        }
    }

    private IEnumerator playMessageInABottle()
    {
        // Stop all other sources
        var allAudioSources = FindObjectsOfType<AudioSource>();
        allAudioSources.ToList().ForEach(a => a.Stop());
        // now play scratch record effect
        audioSource.loop = false;
        audioSource.PlayOneShot(recordScratch);
        yield return new WaitForSeconds(recordScratch.length);
        // then play the message in a bottle 
        audioSource.loop = true;
        audioSource.clip = messageInABottle;
        audioSource.Play();
    }

}

using Assets.Scenes.Common.Scripts;
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
    private MarkSceneManager _markSceneManager;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
        _markSceneManager = GetComponent<MarkSceneManager>();
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
        _markSceneManager.StopAllAudio();
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

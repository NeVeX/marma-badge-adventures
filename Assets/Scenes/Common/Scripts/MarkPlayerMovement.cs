using Assets.Scenes.Common.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkPlayerMovement : MonoBehaviour
{
    [SerializeField] AudioSource PlayerFootsteps;
    [SerializeField] AudioSource JumpSound;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Transform groundChecker;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    void Update()
    {

        if ( !characterController.enabled )
        {
            return; // nothing to do
        }

        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        if ( isGrounded && velocity.y < 0f)
        {
            velocity.y = -2.0f; // force player into the ground so that the player will definitely get "grounded"
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if ( z != 0)
        {
            PlayFootSteps();
        } else
        {
            StopFootsteps();
        }

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Check if jumping
        if ( Input.GetButtonDown("A Button") && isGrounded)
        {
            //Debug.Log("Jumping");
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity); // jump formula/physics
            StopFootsteps();
            if ( JumpSound != null && !JumpSound.isPlaying)
            {
                JumpSound.Play();
            }
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    private void PlayFootSteps()
    {
        if ( PlayerFootsteps != null && !PlayerFootsteps.isPlaying)
        {
            PlayerFootsteps.Play();
        }
    }

    private void StopFootsteps()
    {
        if (PlayerFootsteps != null )
        {
            PlayerFootsteps.Stop();
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float bounceHeight = 2f; // Set the jump height here
    public float bounceDuration = 0.5f; // Set the duration of the jump animation
    public float speed = 5f;
    public float rotationSpeed = 0.2f;
    
    [Header("Needed references")]
    public Camera playerCamera;
    
    [HideInInspector]
    public bool grounded = true;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        if(grounded)
            Bounce();
    }


    private void Move()
    {
        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the camera
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0;
        Vector3 moveDirection = cameraForward.normalized * vertical + playerCamera.transform.right * horizontal;
        
        RotateCharacter(moveDirection);

        bool inJumpPreparation = Input.GetKey(KeyCode.Space) && grounded;

        // Move the character
        if (!inJumpPreparation)
        {
            Vector3 velocity = moveDirection.normalized * speed;
            _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);
        }

        //If no input stop instantly the character
        if (horizontal == 0 && vertical == 0)
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        }
    }

    public void Bounce()
    {
        grounded = false;
        transform.DOMoveY(transform.position.y + bounceHeight, bounceDuration)
            .SetEase(Ease.OutQuad) // Choose an ease function for the jump
            .OnComplete(() =>
            {
                // Return to the ground
                transform.DOMoveY(transform.position.y - bounceHeight, bounceDuration / 2)
                    .SetEase(Ease.InQuad);
                
            });
    }

    public void Jump()
    {
        
    }
    
    
    //Ground check
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void RotateCharacter(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

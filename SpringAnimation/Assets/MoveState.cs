using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : ICharacterState
{
    private CharacterSettings player;
    public void OnEnter()
    {
        player = CharacterSettings.instance;
    }

    public void OnUpdate()
    {
        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the camera
        Vector3 cameraForward = player.playerCamera.transform.forward;
        cameraForward.y = 0; 

        Vector3 moveDirection = cameraForward.normalized * vertical + player.playerCamera.transform.right * horizontal;

        // Rotate the character
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, 
                targetRotation, player.rotationSpeed * Time.deltaTime);
        }

        // Move the character
        Vector3 velocity = moveDirection.normalized * player.speed;
        player.rb.velocity = new Vector3(velocity.x, player.rb.velocity.y, velocity.z);

        if (horizontal == 0 && vertical == 0)
        {
            player.rb.velocity = new Vector3(0, player.rb.velocity.y, 0);
        }
    }

    public void OnExit()
    {
        player.rb.velocity = new Vector3(0, player.rb.velocity.y, 0);
    }
}

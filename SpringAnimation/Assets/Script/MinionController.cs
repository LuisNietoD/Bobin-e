using System;
using MoreMountains.Feedbacks;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Transform mainCamera; // Reference to the main camera
    
    public float followDistance = 3.0f; // Distance between player and minion
    public float smoothness = 0.1f; // Smoothing factor for lerping
    public float yOffset = 1.0f; // Y-axis offset for flying
    public float backwardOffset = 2.0f; // How much further behind the player
    public float hoverSpeed = 1.0f; // Speed of the up and down movement
    public float hoverHeight = 0.5f; // Maximum height of the up and down movement
    public float sideThreshold = 0.1f; // Threshold for camera being on the right or left


    private float hoverOffset;
    private bool cameraOnRight = false;
    

    void LateUpdate()
    {
        // Calculate the up and down movement using a sine wave
        hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Calculate camera position relative to the player
        Vector3 cameraRelativePosition = mainCamera.position - player.position;

        
        
        // Determine if the camera is on the right side (assuming local right direction)
        if(Vector3.Dot(player.right, cameraRelativePosition) > sideThreshold) 
            cameraOnRight = true;
        else if (Vector3.Dot(player.right, cameraRelativePosition) < -sideThreshold)
            cameraOnRight = false;

        // Calculate the target position further behind and to the side of the player
        Vector3 sideOffset = cameraOnRight ? -player.right : player.right;
        Vector3 targetPosition = player.position - player.forward * backwardOffset +
                                 sideOffset * followDistance;

        // Apply the Y-axis offset for flying and the up and down movement
        targetPosition.y = player.position.y + yOffset + hoverOffset;

        // Smoothly move the minion towards the target position using Lerp
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness);

        // Keep the minion looking at the player or adjust its rotation as needed
        // transform.LookAt(player);
    }
}
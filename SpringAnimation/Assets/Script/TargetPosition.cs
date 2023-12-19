using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    public GameObject player;
    public LayerMask groundLayer;
    public float yOffset = 2f; // Adjust this to set the camera's height above the player
    public float movementDuration = 0.4f; // Duration for the movement transition
    public float limit = 3;

    private IEnumerator MoveToGroundCoroutine(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = transform.position;

        while (elapsedTime < movementDuration)
        {
            transform.position = Vector3.Lerp(startingPos, targetPosition, elapsedTime / movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure a smooth ending position
        transform.position = targetPosition;
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = new Ray(player.transform.position, Vector3.down);
        Vector3 targetPos;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, limit, groundLayer))
        {
            // Hit the ground, adjust the camera's position smoothly
            targetPos = hit.point + Vector3.up *yOffset;
        }
        else
        {
            // Didn't hit anything, move camera to player position at the yOffset
            targetPos = player.transform.position + Vector3.down * limit + Vector3.up * yOffset;
        }

        // Smoothly move the camera to the target position
        StartCoroutine(MoveToGroundCoroutine(targetPos));
    }
}

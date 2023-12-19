using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShaker : MonoBehaviour
{
    public Transform cameraTransform; // Reference to your camera's transform
    public float shakeDuration = 0.3f; // Duration of the shake
    public float shakeStrength = 0.5f; // Strength of the shake
    public int vibrato = 10; // Vibrato effect
    public float randomness = 90.0f; // Randomness factor for the shake

    private Vector3 originalPosition; // Store the original position of the camera

    void Start()
    {
        // Store the original position of the camera
        originalPosition = cameraTransform.localPosition;
    }

    public void ShakeCamera()
    {
        GetComponent<ThirdPersonCamera>().vibrating = true;
    }

    // Reset the camera position to its original position
    void ResetCameraPosition()
    {
        cameraTransform.localPosition = originalPosition;
    }

}

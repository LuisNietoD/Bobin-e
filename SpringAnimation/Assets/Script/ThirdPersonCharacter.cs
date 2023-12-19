using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Player's transform
    public float maxDistance = 10.0f; // Maximum distance from the player
    [Space]
    public float sensitivityX = 2.0f; // Camera rotation sensitivity for X-axis
    public float sensitivityY = 2.0f; // Camera rotation sensitivity for Y-axis
    [Space]
    public float offsetX = 0.0f; // Offset on the X-axis
    public float offsetY = 2.0f; // Offset on the Y-axis
    public LayerMask collisionLayers; // Layers to check for collisions

    [Space] public Transform targetTransform;
    
    [Space]
    public float minRotationY = -80;
    public float maxRotationY = 80;

    private float distance = 5.0f; // Distance from the player
    private Vector3 offset; // Initial offset between camera and player
    [Space]
    public float currentX = 0.0f;
    public float currentY = 0.0f;
    
    [Space]
    public bool vibrating;
    public float shakeStrength = 0f;
    public float maxShakeStrength = 1.0f;
    private float elapsedTime = 0;
    
    private void OnEnable()
    {
        MenuManager.OnOpenPauseMenu += LockCameraAndRotateToFront;
    }

    private void OnDisable()
    {
        MenuManager.OnOpenPauseMenu -= LockCameraAndRotateToFront;
    }

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Please assign the player's transform to the camera.");
            return;
        }

        // Calculate initial offset between camera and player
        offset = new Vector3(offsetX, offsetY, -distance);
    }

    void LateUpdate()
    {
        if (GameManager.isPlayerLock)
            return;
        
        // Rotate the camera 
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY -= Input.GetAxis("Mouse Y") * sensitivityY;
        
        // Limit the vertical angle
        currentY = Mathf.Clamp(currentY, minRotationY, maxRotationY); 

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        Vector3 desiredPosition = target.position - rotation * offset;

        // Handle camera collision with the environment
        RaycastHit hit;
        if (Physics.Linecast(target.position, desiredPosition, out hit, collisionLayers))
        {
            distance = Mathf.Clamp(hit.distance, 0f, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }
        
        // Apply the distance offset to the desired position
        desiredPosition = target.position - rotation * new Vector3(offsetX, offsetY, -distance);
        if (vibrating)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeStrength;
            desiredPosition += randomOffset;
        }
        transform.position = desiredPosition;

        // Look at the player's position
        transform.LookAt(target.position + Vector3.up * offsetY);
        
        if (vibrating)
        {
            elapsedTime += Time.deltaTime;
            shakeStrength = Mathf.Lerp(0.0f, maxShakeStrength, Mathf.Clamp01(elapsedTime / 2));
        }
        else // Reset shake strength when vibrating is false
        {
            elapsedTime = 0;
            shakeStrength = 0.0f;
        }
    }

    public IEnumerator Vibrate(float shake, float time)
    {
        shakeStrength = shake;
        vibrating = true;
        yield return new WaitForSeconds(time);
        shakeStrength = 0;
        vibrating = false;
    }
    
    private void LockCameraAndRotateToFront()
    {
        GameManager.isPlayerLock = true;

        // Get the target position and rotation from the provided transform
        Vector3 targetPosition = targetTransform.position;
        Quaternion targetRotation = targetTransform.rotation;

        // Tween camera rotation and movement
        transform.DOMove(targetPosition, 0.5f)
            .SetEase(Ease.InOutCubic)
            .OnUpdate(() => UpdateCameraRotation(targetRotation))
            .OnComplete(GuiOpener.OpenPauseMenu);
    }
    
    private void UpdateCameraRotation(Quaternion targetRotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }
}

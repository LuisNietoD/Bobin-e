using System;
using System.Collections;
using RotaryHeart.Lib.PhysicsExtension;
using UnityEngine;
using Physics = UnityEngine.Physics;

public class Movement : MonoBehaviour
{
    [Header("Settings")]
    public float jumpForce = 10f;
    public float speed = 5f;
    public float rotationSpeed = 0.2f;
    public float retractDuration = 0.3f;
    public float gravity = -9.81f;

    [Space]
    [Header("Needed references")]
    public Camera playerCamera;
    public MeshMorphing spring;

    [Space]
    [Header("Raycast settings")]
    public float maxRayDistance = 2f; 
    public float minRayDistance = 0.5f;

    private bool retracted = true;
    public bool bounced = false;
    
    private float restWeight = 0.1f;      // Weight for the rest position
    private float compressedWeight = 0f;  // Weight for the compressed position
    private float extendedWeight = 0.3f;  // Weight for the extended position

    private Coroutine retractCoroutine;    // Coroutine to retract the spring


    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        
        RetractTest();
        
        Physics.gravity = new Vector3(0, gravity, 0);
    }

    /// <summary>
    /// Move the player
    /// </summary>
    void Move()
    {
        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the camera
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0; 

        Vector3 moveDirection = cameraForward.normalized * vertical + playerCamera.transform.right * horizontal;

        // Rotate the character
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move the character
        Vector3 velocity = moveDirection.normalized * speed;
        _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);
    }

    void RetractTest()
    {
        float thickness = 0.5f;
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down;
        RaycastHit hit;
        
        
        if (Physics.SphereCast(rayOrigin,thickness, rayDirection, out hit, maxRayDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            Debug.DrawLine(rayOrigin, hit.point, Color.red);
            if (hitObject.CompareTag("Ground") && bounced)
            {
                //Extend the spring before jumping
                retracted = false;
                float weight = Mathf.Lerp(compressedWeight, extendedWeight, Mathf.InverseLerp(minRayDistance, maxRayDistance, hit.distance));
                weight = Mathf.Clamp(weight, compressedWeight, extendedWeight);
                spring.m_MorphTargets[0].Weight = weight;
            }
            else if (hitObject.CompareTag("Ground"))
            {
                //Only retract the spring before landing
                float weight = Mathf.Lerp(extendedWeight, compressedWeight, Mathf.InverseLerp(minRayDistance, maxRayDistance, hit.distance));
                weight = Mathf.Clamp(weight, compressedWeight, spring.m_MorphTargets[0].Weight);
                spring.m_MorphTargets[0].Weight = weight;
            }
        }
        else if (!retracted)
        {
            retracted = true;
            bounced = false;
            // Stop the coroutine if exist
            if (retractCoroutine != null)
            {
                StopCoroutine(retractCoroutine);
            }

            retractCoroutine = StartCoroutine(RetractSpring());
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //Start extend and jump when collide the ground
        if (collision.gameObject.CompareTag("Ground") && !bounced)
        {
            _rb.velocity = new Vector3(-_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            bounced = true;
        }
    }
    
    
    //retract animation curve based
    IEnumerator RetractSpring()
    {
        float elapsedTime = 0f;

        //retract over time
        while (elapsedTime < retractDuration)
        {
            float weight = Mathf.Lerp(extendedWeight, restWeight, elapsedTime / retractDuration);
            spring.m_MorphTargets[0].Weight = weight;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the weight to rest weight
        spring.m_MorphTargets[0].Weight = restWeight;
        
        retractCoroutine = null;
    }
}
